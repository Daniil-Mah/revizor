using System;

namespace Revizor_Service.Responses;

public class UserReponse
{
    public Guid Id {get;set;}
    public string Name { get; set; }
    public int RoleId {get;set;} 
    public string Password {get;set;} 
    public DateTime CreatedAt {get;set;}
}
