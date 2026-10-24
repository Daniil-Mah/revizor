using System;

namespace Revizor_Service.Database.Models;

public class User
{
    public Guid Id {get;set;}
    public string FirstName { get; set;}
    public string SecondName { get; set;}
    public string LastName { get; set;}
    public int RoleId {get;set;} 
    public Role? Role { get; set; }
    public string Password {get;set;} 
    public DateTime CreatedAt {get;set;}
    public string? RefreshToken {get;set;}
    public DateTime? RefreshExpireTime { get; set; }

    public string GetFullName() => $"{SecondName} {FirstName} {LastName}";
}
