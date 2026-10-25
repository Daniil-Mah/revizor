using System;

namespace Revizor_Service.Responses;

public class UserReponse
{
    public Guid Id {get;set;}
    public string FullName { get; set; }
    public string Role {get;set;} 
    public DateTime CreatedAt {get;set;}

}
