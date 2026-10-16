using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Npgsql;
using Revizor_Service.Database;
using Revizor_Service.Database.Models;
using Revizor_Service.DTO;
using Revizor_Service.Responses;
using Revizor_Service.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddScoped<DBRepo>();
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
    opt.AddPolicy("Admin",p => p.RequireRole("Admin"));
    opt.AddPolicy("Revizor", p=> p.RequireRole("Revizor"));
});
builder.Services.AddScoped<IPasswordHasher<CreateUser>, PasswordHasher<CreateUser>>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var ex = feature?.Error;
        
        var (statusCode, message, code) = ex switch
        {
            PostgresException { SqlState: "23505" } => (409, "Запись уже существует", ExceptionCode.UniqueViolation),
            PostgresException { SqlState: "23503" } => (400, "Связанная запись не найдена", ExceptionCode.ForeignKeyViolation),
            PostgresException { SqlState: "23514" } => (400, "Нарушено ограничение", ExceptionCode.CheckViolation),
            PostgresException { SqlState: "23502" } => (400, "Обязательное поле не заполнено", ExceptionCode.NotNullViolation),
            PostgresException => (500, "Ошибка базы данных", ExceptionCode.InternalError),
            _ => (500, "Внутренняя ошибка сервера", ExceptionCode.InternalError)
        };
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

app.MapGet("/roles", async (DBRepo db) =>
{
    var roles = await db.GetEntities<Role>("role");
    return Results.Ok(ApiResponses<IEnumerable<Role>>.Ok(roles, new Pagination{}));
}).RequireAuthorization("Admin");

app.MapPost("/roles/", async (DBRepo db, CreateRole dto) =>
{
    await db.CreateEntity("role",dto);
    return Results.Ok(ApiResponses.Ok("Role is created"));
}).RequireAuthorization("Admin");

app.MapDelete("/roles/{id}", async (DBRepo db, int id) =>
{
    int success = await db.DeleteEntity("role",id);
    return success > 0? Results.Ok(ApiResponses.Ok("Role is delete")) : Results.NotFound(ApiResponses.Fail("Role not found", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapGet("/roles/{id}", async (DBRepo db, int id) =>
{
    var role = await db.GetEntity<Role>("role",id);
    return role != null ? Results.Ok(ApiResponses<Role>.Ok(data: role)) : Results.NotFound(ApiResponses.Fail("Role does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapPatch("/roles/{id}",async (DBRepo db, int id, PatchRole dto ) =>
{
    var success = await db.PatchEntity("role",dto, id);
    return success > 0? Results.Ok(ApiResponses.Ok("Role is edit")): Results.NotFound(ApiResponses.Fail("Role does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapGet("/users", async (DBRepo db) =>
{
    return Results.Ok(ApiResponses<IEnumerable<UserReponse>>.Ok(data: await db.GetEntities<UserReponse>("users")));
}).RequireAuthorization("Admin");

app.MapPost("/users/", async (IPasswordHasher<CreateUser> hasher,DBRepo db, CreateUser dto) =>
{
    dto.Password = hasher.HashPassword(dto, dto.Password);
    await db.CreateEntity("Users",dto);
    return Results.Ok(ApiResponses.Ok("User is created"));
}).RequireAuthorization("Admin");

app.MapDelete("/users/{id}", async (DBRepo db, Guid id) =>
{
    int success = await db.DeleteEntity("users",id);
    return success > 0? Results.Ok(ApiResponses.Ok("User is delete")) : Results.NotFound(ApiResponses.Fail("User not found", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapGet("/users/{id}", async (DBRepo db, Guid id) =>
{
    var user = await db.GetEntity<UserReponse>("Users",id);
    return user != null ? Results.Ok(ApiResponses<UserReponse>.Ok(data: user)) : Results.NotFound(ApiResponses.Fail("User does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapPatch("/users/{id}",async (DBRepo db, Guid id, PatchUser dto ) =>
{
    var success = await db.PatchEntity("Users",dto, id);
    return success > 0? Results.Ok(ApiResponses.Ok("User is edit")): Results.NotFound(ApiResponses.Fail("User does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapPost("/login", async (DBRepo db, IPasswordHasher<User> hasher, IConfiguration cfg, HttpContext httpContext, Login dto) =>
{
    var user = await db.GetEntityByName<User>("users", dto.Name);
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
    await db.PatchEntity("users",user,user.Id);

    var role = await db.GetEntity<Role>("role",user.RoleId);
    if (role == null) {return Results.NotFound(ApiResponses.Fail("role not found",ExceptionCode.NotFound));}
    
    user.Role=role;
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

app.MapPost("/refresh", async (DBRepo db, HttpContext httpContext, IConfiguration cfg) =>
{
    var refreshToken = httpContext.Request.Cookies["refreshToken"];
    if (refreshToken == null)
    {
        return Results.NotFound(ApiResponses.Fail("Cookie not found", ExceptionCode.NotFound));
    }
    var user = await db.GetUserByRefresh(refreshToken);
    if (user==null)
    {
        return Results.NotFound(ApiResponses.Fail("user not found", ExceptionCode.NotFound));
    }
    user.RefreshToken = RefreshToken.GenerateRefreshToken();
    user.RefreshExpireTime = DateTime.UtcNow.AddDays(7);
    await db.PatchEntity("users",user,user.Id);

    var role = await db.GetEntity<Role>("role",user.RoleId);
    if (role == null) {return Results.NotFound(ApiResponses.Fail("role not found",ExceptionCode.NotFound));}
    user.Role=role;
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

app.MapGet("/users/me", async (DBRepo db, ClaimsPrincipal claims) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid user);
    if (!success) {return Results.Unauthorized();}
    return Results.Ok(ApiResponses<UserReponse>.Ok(await db.GetEntity<UserReponse>("users", user)));
}).RequireAuthorization();

app.MapPost("/users/me/change-password/", async (DBRepo db, ClaimsPrincipal claims, IPasswordHasher<User> hasher, string pwd) =>
{   
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    if (!success)
    {
        return Results.NotFound();
    }
    var user = await db.GetEntity<User>("Users", userId); 
    if (user == null)
    {
        return Results.NotFound();
    }
    user.Password = hasher.HashPassword(user, pwd);
    await db.PatchEntity("users",user,user.Id);
    return Results.Ok(ApiResponses.Ok("Password edit"));
}).RequireAuthorization("Admin");

app.MapGet("/facilities", async (DBRepo db) =>
{
    return Results.Ok(ApiResponses<IEnumerable<Facility>>.Ok(await db.GetEntities<Facility>("facility")));
}).RequireAuthorization("Admin");

app.MapPost("/facilities/", async (DBRepo db, CreateFacility dto) =>
{
    await db.CreateEntity("Facility",dto);
    return Results.Ok(ApiResponses.Ok("Facility is created"));
}).RequireAuthorization("Admin");

app.MapDelete("/facilities/{id}", async (DBRepo db, Guid id) =>
{
    int success = await db.DeleteEntity("Facility",id);
    return success > 0? Results.Ok(ApiResponses.Ok("Facility is delete")) : Results.NotFound(ApiResponses.Fail("Facility not found", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapGet("/facilities/{id}", async (DBRepo db, Guid id) =>
{
    var role = await db.GetEntity<Facility>("Facility",id);
    return role != null ? Results.Ok(ApiResponses<Facility>.Ok(data: role)) : Results.NotFound(ApiResponses.Fail("Facility does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapPatch("/facilities/{id}",async (DBRepo db, Guid id, PatchFacility dto ) =>
{
    var success = await db.PatchEntity("Facility",dto, id);
    return success > 0? Results.Ok(ApiResponses.Ok("Facility is edit")): Results.NotFound(ApiResponses.Fail("Facility does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapGet("/plan", async (DBRepo db) =>
{
    return Results.Ok(ApiResponses<IEnumerable<Plan>>.Ok(await db.GetEntities<Plan>("plan")));
}).RequireAuthorization("Admin");

app.MapGet("/plan/{id]}", async (DBRepo db, Guid id) =>
{
    var plan = await db.GetEntity<Plan>("plan",id);
    return plan != null? Results.Ok(ApiResponses<Plan>.Ok(plan)) : Results.NotFound(ApiResponses.Fail("plan not found", ExceptionCode.NotFound));
}).RequireAuthorization("Admin");

app.MapPost("/plan/", async (DBRepo db, ClaimsPrincipal claims, CreatePlan dto) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
    await db.CreateEntity("Plan",dto);
    return Results.Ok(ApiResponses.Ok("Plan is created"));
}).RequireAuthorization("Admin");

app.MapPatch("/plan/{id}",async (DBRepo db, Guid id, PatchPlan dto ) =>
{
    var success = await db.PatchEntity("Plan",dto, id);
    return success > 0? Results.Ok(ApiResponses.Ok("Plan is edit")): Results.NotFound(ApiResponses.Fail("Plan does not exists", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapDelete("/plan/{id}", async (DBRepo db, Guid id) =>
{
   await db.DeleteEntity("plan", id);
   return Results.Ok(ApiResponses.Ok("Plan delete")); 
}).RequireAuthorization("Admin");

app.MapGet("/plan/current", async (DBRepo db) =>
{
    var currentPlan = await db.GetCurrentPlan();
    return currentPlan != null? Results.Ok(ApiResponses<Plan>.Ok(currentPlan)): Results.NotFound(ApiResponses.Fail("plan not found", ExceptionCode.NotFound));
}).RequireAuthorization("Admin");

app.MapPost("/plan/archived", async (DBRepo db, Guid id) =>
{
    int success = await db.ArchivedPlan();
    return success > 0? Results.Ok(ApiResponses.Ok("Plan is arcived")) : Results.NotFound(ApiResponses.Fail("Plan not found", ExceptionCode.DoesNotExists));
}).RequireAuthorization("Admin");

app.MapGet("/plan-assigment/{id}", async (DBRepo db, Guid id) =>
{
    await db.GetEntity<PlanAssigment>("planassigment", id);
    return Results.Ok(ApiResponses.Ok("Plan Assigment create")); 
}).RequireAuthorization("Admin");

app.MapPost("/plan-assigment/{id}", async (DBRepo db, Guid id , List<CreateAssigment> dtos) =>
{
    await db.CreateEntity("planassigment",dtos);
    return Results.Ok(ApiResponses.Ok("Plan Assigment create")); 
}).RequireAuthorization("Admin");

app.MapPost("/record/{id}", async (DBRepo db, Guid id, ClaimsPrincipal claims,CreateRecord dto) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid user);
    if (!success) {return Results.NotFound(ApiResponses.Fail("user not found", ExceptionCode.NotFound));}
    dto.UserId = user;
    var planId = await db.GetEntity<Plan>("plan",id);
    if (planId != null) { return Results.NotFound(ApiResponses.Fail("planID not found", ExceptionCode.NotFound));}
    dto.PlanId = id;
    await db.CreateEntity("record", dto);  
    return Results.Ok(ApiResponses.Ok("record add"));
}).RequireAuthorization("Admin","Revizor");

app.MapPatch("/record/{id}", async (DBRepo db, ClaimsPrincipal claims,Guid id, PatchRecord dto) =>
{
    var success = Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out Guid user);
    if (!success) {return Results.NotFound(ApiResponses.Fail("user not found", ExceptionCode.NotFound));}
    var result = await db.PatchEntity("record",dto,id);
    return result>0? Results.Ok(ApiResponses.Ok("record is edit")) : Results.NotFound(ApiResponses.Fail("record not found",ExceptionCode.NotFound));    
});

app.MapGet("/admin/reports", () =>
{
    Reports.GetReports();
    string filePath = Path.GetFullPath("./Excel/Reports/10_2026.xlsx"); // Нужен абсолютный путь
    string contentType = "application/pdf";
    string fileName = "10_2026.xlsx";
    return Results.File(filePath,contentType,fileName);
});

app.Run();
