using Npgsql;
using Microsoft.EntityFrameworkCore;
using Revizor_Service.Services;
using System.Text.Json;

namespace Revizor_Service.Responses;

public static class ErrorHandler
{
    public static (int Status, string Message, ExceptionCode Code) GetError(Exception? ex)
    {
        if (ex is BadHttpRequestException )
        {
            return (400, "Невалидный формат данных", ExceptionCode.ValidationError);
        }

        if (ex is JsonException )
        {
            return (400, "Ошибка парсинга JSON", ExceptionCode.ValidationError);
        }

        if (ex is DbUpdateException dbEx && dbEx.InnerException is PostgresException innerPg)
        {
            ex = innerPg;
        }
        
        if (ex is PostgresException pg)
        {
            if (pg.SqlState == "23505")
                return (409, "Запись уже существует", ExceptionCode.UniqueViolation);
            
            if (pg.SqlState == "23503")
                return (400, "Связанная запись не найдена", ExceptionCode.ForeignKeyViolation);
            
            if (pg.SqlState == "23514")
                return (400, "Нарушено ограничение", ExceptionCode.CheckViolation);
            
            if (pg.SqlState == "23502")
                return (400, "Обязательное поле не заполнено", ExceptionCode.NotNullViolation);
            
            return (500, "Ошибка базы данных", ExceptionCode.InternalError);
        }
        
        return (500, "Внутренняя ошибка сервера", ExceptionCode.InternalError);
    }
}