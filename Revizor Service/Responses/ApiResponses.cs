
using Revizor_Service.Services;

namespace Revizor_Service.Responses;

public class ApiResponses<T> : ApiResponses
{
    public T? Data {get;set;}
    public Pagination? Pagination {get;set;}
    public static ApiResponses<T> Ok(T data, Pagination? pagination = null) => new() { Success = true, Data = data , Pagination = pagination};
}

public class ApiResponses
{
    public bool Success {get;set;}
    public string? Message { get; set; }
    public ExceptionCode? Code { get; set; }

    public static ApiResponses Ok(string? message = null) => new() { Success = true, Message = message };
    public static ApiResponses Fail(string message, ExceptionCode code) => new() {Success = false, Message=message, Code = code};
}

public class Pagination
{
    public int Total { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public Pagination(){}
    public Pagination(int limit, int offset, int total)
    {
        Limit = limit; Offset = offset; Total = total;
    }
}