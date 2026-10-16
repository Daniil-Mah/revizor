using System;
using System.Text.Json.Serialization;

namespace Revizor_Service.Services;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExceptionCode
{
    DoesNotExists,
    ValidationError,
    NotFound,
    UniqueViolation,
    ForeignKeyViolation,
    CheckViolation,
    NotNullViolation,
    Unauthorized,
    Forbidden,
    InternalError
}
