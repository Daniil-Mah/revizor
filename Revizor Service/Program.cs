using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Npgsql;
using Revizor_Service.Database;
using Revizor_Service.Database.Models;
using Revizor_Service.DTO;
using Revizor_Service.Responses;
using Revizor_Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("revizor")));
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name="Authoriation",
        Description="JWT",
        In=ParameterLocation.Header,
        Type=SecuritySchemeType.Http,
        Scheme=JwtBearerDefaults.AuthenticationScheme,
        BearerFormat="JWT"
    });
    opt.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer",doc)] =[] 
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateLifetime=true,
        ValidateIssuer=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer=builder.Configuration["JWT:Issuer"],
        ValidAudience=builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)), 
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin",p => p.RequireRole("admin"));
    opt.AddPolicy("Revizor", p=> p.RequireRole("revizor"));
});
builder.Services.AddScoped<IPasswordHasher<CreateUser>, PasswordHasher<CreateUser>>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<ReportsServices>();
var app = builder.Build();
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        var (statusCode, message, code) = ErrorHandler.GetError(ex);
        
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(ApiResponses.Fail(message, code));
    });
});
app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/roles", async (AppDbContext db) => Results.Ok(ApiResponses<IEnumerable<Role>>.Ok(await db.Roles.ToArrayAsync(), new Pagination{}))).RequireAuthorization("Admin");

app.MapPost("/roles/", async (AppDbContext db, CreateRole dto) =>
{
    await db.Roles.AddAsync(new Role {Name=dto.Name.ToLower()});
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("Role is created"));
}).RequireAuthorization("Admin");

app.MapDelete("/roles/{roleId}", async (AppDbContext db, int roleId) =>
{
    var role = await db.Roles.FirstOrDefaultAsync(r => r.Id== roleId);
    if (role == null) return Results.NotFound(ApiResponses.Fail("Role not found", ExceptionCode.DoesNotExists));
    db.Roles.Remove(role);
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("Role is delete"));
}).RequireAuthorization("Admin");

app.MapGet("/roles/{roleId}", async (AppDbContext db, int roleId) =>
{
    var role = await db.Roles.FirstOrDefaultAsync(r=> r.Id==roleId);
    return role != null ? Results.Ok(ApiResponses<Role>.Ok(data: role)) : Results.NotFound(ApiResponses.Fail("Role does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapPatch("/roles/{roleId}",async (AppDbContext db, int roleId, PatchRole dto ) =>
{
    var role = await db.Roles.FirstOrDefaultAsync(r=> r.Id == roleId);
    if (role == null) {return Results.NotFound(ApiResponses.Fail("Role does not exists", ExceptionCode.DoesNotExists));}
    if (dto.Name != null) role.Name=dto.Name; 
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("Role is edit"));
}).RequireAuthorization("Admin");

app.MapGet("/users", async (AppDbContext db) => Results.Ok(ApiResponses<IEnumerable<User>>.Ok(data: await db.Users.ToArrayAsync()))).RequireAuthorization("Admin");

app.MapPost("/users/", async (IPasswordHasher<CreateUser> hasher,AppDbContext db, CreateUser dto) =>
{
    dto.Password = hasher.HashPassword(dto, dto.Password);
    await db.Users.AddAsync(new User {Name = dto.Name, Password = dto.Password, RoleId = dto.RoleId });
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("User is created"));
}).RequireAuthorization("Admin");

app.MapDelete("/users/{userId}", async (AppDbContext db, Guid userId) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u=> u.Id == userId);
    if (user==null) {return Results.NotFound(ApiResponses.Fail("User not found", ExceptionCode.DoesNotExists));}
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("User is delete"));
}).RequireAuthorization("Admin");

app.MapGet("/users/{userId}", async (AppDbContext db, Guid userId) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u=> u.Id == userId);
    return user != null ? Results.Ok(ApiResponses<User>.Ok(data: user)) : Results.NotFound(ApiResponses.Fail("User does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapPatch("/users/{userId}",async (AppDbContext db, Guid userId, PatchUser dto ) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u=> u.Id == userId);
    if (user == null) { return Results.NotFound(ApiResponses.Fail("User does not exists", ExceptionCode.DoesNotExists));}

    if (dto.Name != null) user.Name=dto.Name;
    if (dto.RoleId != null) user.RoleId = (int)dto.RoleId;

    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("User is edit"));
}).RequireAuthorization("Admin");

app.MapPost("/login", async (AppDbContext db, IPasswordHasher<User> hasher, IConfiguration cfg, HttpContext httpContext, Login dto) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u=> u.Name == dto.Name);
    if (user==null)
    {
        return Results.Unauthorized();
    }
    if (hasher.VerifyHashedPassword(user,user.Password, dto.Password.ToString()) != PasswordVerificationResult.Success)
    {
        return Results.Unauthorized();
    }
    user.RefreshToken = RefreshToken.GenerateRefreshToken();
    user.RefreshExpireTime = DateTime.UtcNow.AddDays(7);
    var role = await db.Roles.Where(r=> r.Id == user.RoleId).FirstAsync();    
    user.Role=role;

    await db.SaveChangesAsync();
    httpContext.Response.Cookies.Append("refreshToken", user.RefreshToken,new CookieOptions
    {
        HttpOnly=true,
        Secure=true,
        SameSite=SameSiteMode.Strict,
        Expires=DateTime.UtcNow.AddDays(7)
    });

    return Results.Ok(
        ApiResponses<Token>.Ok(
            new (){ 
                AccessToken = JWT.GenToken(user,cfg)
                }
            )
        );
});

app.MapPost("/refresh", async (AppDbContext db, HttpContext httpContext, IConfiguration cfg) =>
{
    var refreshToken = httpContext.Request.Cookies["refreshToken"];
    if (refreshToken == null)
    {
        return Results.NotFound(ApiResponses.Fail("Cookie not found", ExceptionCode.NotFound));
    }
    var user = await db.Users.FirstOrDefaultAsync(u=> u.RefreshToken == refreshToken);
    if (user==null)
    {
        return Results.NotFound(ApiResponses.Fail("user not found", ExceptionCode.NotFound));
    }
    user.RefreshToken = RefreshToken.GenerateRefreshToken();
    user.RefreshExpireTime = DateTime.UtcNow.AddDays(7);
    await db.SaveChangesAsync();

    httpContext.Response.Cookies.Append("refreshToken", user.RefreshToken , new CookieOptions
    {
        HttpOnly=true,
        Secure=true,
        SameSite=SameSiteMode.Strict,
        Expires=DateTime.UtcNow.AddDays(7), 
        Path="/"
    });
    return Results.Ok(ApiResponses<Token>.Ok(new () {AccessToken=JWT.GenToken(user, cfg)}));
});

app.MapGet("/users/me", async (AppDbContext db, ClaimsPrincipal claims) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    if (!success) {return Results.Unauthorized();}
    var user = await db.Users.FirstOrDefaultAsync(u=> u.Id == userId);
    if (user == null) return Results.Unauthorized();
    return Results.Ok(ApiResponses<User>.Ok(user));
}).RequireAuthorization("Admin", "Revizor");

app.MapPost("/users/me/change-password/", async (AppDbContext db, ClaimsPrincipal claims, IPasswordHasher<User> hasher, string pwd) =>
{   
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    if (!success) return Results.NotFound();

    var user = await db.Users.FirstOrDefaultAsync(u=> u.Id == userId); 
    if (user == null) return Results.NotFound();

    user.Password = hasher.HashPassword(user, pwd);
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("Password edit"));
}).RequireAuthorization("Admin", "Revizor");

app.MapGet("/facilities", async (AppDbContext db) => Results.Ok(ApiResponses<IEnumerable<Facility>>.Ok(await db.Facilities.ToArrayAsync()))).RequireAuthorization("Admin");

app.MapPost("/facilities/", async (AppDbContext db, CreateFacility dto) =>
{
    await db.Facilities.AddAsync(new Facility{Title = dto.Title, Location = dto.Location});
    return Results.Ok(ApiResponses.Ok("Facility is created"));
}).RequireAuthorization("Admin");

app.MapDelete("/facilities/{facilityId}", async (AppDbContext db, Guid facilityId) =>
{
    var facility = await db.Facilities.FirstOrDefaultAsync(f => f.Id==facilityId);
    if (facility == null) return Results.NotFound(ApiResponses.Fail("Facility not found", ExceptionCode.DoesNotExists));
    db.Facilities.Remove(facility);
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("Facility is delete"));
}).RequireAuthorization("Admin");

app.MapGet("/facilities/{facilityId}", async (AppDbContext db, Guid facilityId) =>
{
    var facility = await db.Facilities.FirstOrDefaultAsync(f=> f.Id==facilityId);
    if (facility == null) return Results.NotFound(ApiResponses.Fail("Facility does not exists", ExceptionCode.DoesNotExists));
    return Results.Ok(ApiResponses<Facility>.Ok(data: facility));
}).RequireAuthorization("Admin");

app.MapPatch("/facilities/{facilityId}",async (AppDbContext db, Guid facilityId, PatchFacility dto ) =>
{
    var facility = await db.Facilities.FirstOrDefaultAsync(f=> f.Id == facilityId);
    if (facility == null) return Results.NotFound(ApiResponses.Fail("Facility does not exists", ExceptionCode.DoesNotExists));

    if (dto.Title != null) facility.Title = dto.Title;
    if (dto.Location != null) facility.Location = dto.Location;

    return Results.NoContent();
}).RequireAuthorization("Admin");

app.MapGet("/plan", async (AppDbContext db) => Results.Ok(ApiResponses<IEnumerable<Plan>>.Ok(await db.Plans.ToArrayAsync()))    ).RequireAuthorization("Admin");

app.MapGet("/plan/{planId}", async (AppDbContext db, Guid planId) =>
{
    var plan = await db.Plans.FirstOrDefaultAsync(p=> p.Id == planId);
    return plan != null? Results.Ok(ApiResponses<Plan>.Ok(plan)) : Results.NotFound(ApiResponses.Fail("plan not found", ExceptionCode.NotFound));
}).RequireAuthorization("Admin");

app.MapPost("/plan/", async (AppDbContext db, ClaimsPrincipal claims, CreatePlan dto) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    if (!success) return  Results.Unauthorized();
    await db.Plans.AddAsync(new Plan{Name=dto.Name, PeriodStart=dto.PeriodStart, PeriodEnd=dto.PeriodEnd, CreatedById=userId});
    await db.SaveChangesAsync();
    return Results.Created("/plan/",ApiResponses.Ok("Plan is created"));
}).RequireAuthorization("Admin");

app.MapPatch("/plan/{planId}",async (AppDbContext db, Guid planId,ClaimsPrincipal claims, PatchPlan dto ) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    if (!success) return Results.Unauthorized();

    var plan = await db.Plans.FirstOrDefaultAsync(p=>p.Id==planId);
    if (plan==null) return Results.NotFound(ApiResponses.Fail("plan not found",ExceptionCode.NotFound));
    
    if (dto.Name != null) plan.Name = dto.Name;
    if (dto.CreatedAt != null) plan.CreatedAt = (DateTime)dto.CreatedAt;
    if (dto.CreatedBy != null) plan.CreatedById = (Guid)dto.CreatedBy;
    if (dto.PeriodStart != null) plan.PeriodStart = (DateTime)dto.PeriodStart;
    if (dto.PeriodEnd != null) plan.PeriodEnd = (DateTime) dto.PeriodEnd;
    await db.SaveChangesAsync();
    return Results.NoContent();
}).RequireAuthorization("Admin");

app.MapDelete("/plan/{planId}", async (AppDbContext db, Guid planId) =>
{
    var plan = await db.Plans.FirstOrDefaultAsync(p=> p.Id == planId);
    if (plan == null) return Results.NotFound(ApiResponses.Fail("plan not found", ExceptionCode.NotFound));
    db.Plans.Remove(plan);
    return Results.NoContent();     
}).RequireAuthorization("Admin");

app.MapGet("/plan/current", async (AppDbContext db) =>
{
    var currentPlan = await db.Plans.FirstOrDefaultAsync(p=>p.IsArchived==false);
    if (currentPlan == null) return Results.NotFound(ApiResponses.Fail("current plan not found",ExceptionCode.NotFound));
    var result = await db.PlanAssigments.Where(r=> r.PlanId == currentPlan.Id).ToListAsync();;
    return Results.Ok(ApiResponses<IEnumerable<PlanAssigment>>.Ok(result));
}).RequireAuthorization("Admin");

app.MapPost("/plan/archived", async (AppDbContext db) =>
{
    var plan = await db.Plans.FirstOrDefaultAsync(p=> p.IsArchived == false);
    if (plan == null) return Results.NotFound(ApiResponses.Fail("plan not found",ExceptionCode.NotFound));
    plan.IsArchived=true;
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("Plan is arcived"));
}).RequireAuthorization("Admin");

app.MapPost("/plan-assigment/{planId}", async (AppDbContext db,ClaimsPrincipal claims, Guid planId, List<CreateAssigment> dtos) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    if (!success) return Results.Unauthorized();
    foreach (var assigment in dtos)
    {
        await db.PlanAssigments.AddAsync(new PlanAssigment{
            PlanId = assigment.PlanId,
            PassById = assigment.PassById,
            PassAt = assigment.PassAt,
            FacilityId = assigment.FacilityId,
            ResponsiblePersonId = assigment.ResponsiblePersonId});
    }
    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("Plan Assigments create")); 
}).RequireAuthorization("Admin");

app.MapPost("/record/", async (AppDbContext db, ClaimsPrincipal claims,CreateRecord dto) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    if (!success) {return Results.NotFound(ApiResponses.Fail("user not found", ExceptionCode.NotFound));}
    var plan = await db.Plans.FirstOrDefaultAsync(p => p.IsArchived==false);
    if (plan == null) return Results.NotFound(ApiResponses.Fail("plan not found", ExceptionCode.NotFound));
    await db.Records.AddAsync(new Record {
        PlanId= plan.Id,
        FacilityId=dto.FacilityId, 
        UserId=userId, 
        StartAt=dto.StartAt,
        EndAt=dto.EndAt,
        PassageWay=dto.PassageWay});  
    return Results.Ok(ApiResponses.Ok("record add"));
}).RequireAuthorization("Admin","Revizor");

app.MapPatch("/record/{recordId}", async (AppDbContext db, ClaimsPrincipal claims,Guid recordId, PatchRecord dto) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    if (!success) {return Results.NotFound(ApiResponses.Fail("user not found", ExceptionCode.NotFound));}

    var record = await db.Records.FirstOrDefaultAsync(r=> r.Id == recordId);
    if (record == null) return Results.NotFound(ApiResponses.Fail("record not found",ExceptionCode.NotFound));

    if (dto.PlanId != null) record.PlanId = (Guid)dto.PlanId;
    if (dto.FacilityId != null) record.FacilityId = (Guid)dto.FacilityId;
    if (dto.UserId != null) record.UserId = (Guid)dto.UserId;
    if (dto.StartAt != null) record.StartAt = (TimeOnly)dto.StartAt;
    if (dto.EndAt != null) record.EndAt = (TimeOnly)dto.EndAt;
    if (dto.PassageWay != null) record.PassageWay = (float)dto.PassageWay;

    await db.SaveChangesAsync();
    return Results.Ok(ApiResponses.Ok("record is edit"));
}).RequireAuthorization("Admin","Revizor");

app.MapGet("/generate-reports/{planId}", async (AppDbContext db,ReportsServices reportsServices, ClaimsPrincipal claims, Guid planId) =>
{
    var role = claims.FindFirstValue(ClaimTypes.Role);
    
    string filePath = Path.GetFullPath("./Excel/Reports/10_2026.xlsx"); 
    string contentType = "application/pdf";
    string fileName = "10_2026.xlsx";
    return Results.File(filePath,contentType,fileName);
}).RequireAuthorization("Admin");

app.Run();