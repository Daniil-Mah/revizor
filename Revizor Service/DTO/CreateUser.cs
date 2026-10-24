using System;
using Revizor_Service.Database.Models;

namespace Revizor_Service.DTO;

public class CreateUser
{
    public string FirstName { get; set;}
    public string SecondName { get; set;}
    public string LastName { get; set;}
    public int RoleId {get;set;} 
    public string Password {get;set;} 
}
