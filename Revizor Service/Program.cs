using Microsoft.AspNetCore.Diagnostics;
using Npgsql;
using Revizor_Service.Database;
using Revizor_Service.Database.Models;
using Revizor_Service.DTO;
using Revizor_Service.Responses;
using Revizor_Service.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddScoped<DBRepo>();
builder.Services.AddSwaggerGen();

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

app.MapGet("/roles", async (DBRepo db) =>
{
    return Results.Ok(await db.GetEntities<Role>("role"));
});

app.MapPost("/roles/", async (DBRepo db, CreateRole dto) =>
{
    await db.CreateEntity("role",dto);
    return Results.Ok(ApiResponses.Ok("Role is created"));
});

app.MapDelete("/roles/{id}", async (DBRepo db, Guid id) =>
{
    int success = await db.DeleteEntity("role",id);
    return success > 0? Results.Ok(ApiResponses.Ok("Roles is delete")) : Results.NotFound(ApiResponses.Fail("Role not found", ExceptionCode.DoesNotExists));
});

app.MapGet("/roles/{id}", async (DBRepo db, Guid id) =>
{
    var role = await db.GetEntity<Role>("role",id);
    return role != null ? Results.Ok(ApiResponses<Role>.Ok(data: role)) : Results.BadRequest(ApiResponses.Fail("Role does not exists", ExceptionCode.DoesNotExists));
});

app.MapPatch("/roles/{id}",async (DBRepo db, Guid id, PatchRole dto ) =>
{
    
});
app.Run();
