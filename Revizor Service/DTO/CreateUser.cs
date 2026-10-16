using System;
using Revizor_Service.Database.Models;

namespace Revizor_Service.DTO;

public class CreateUser
{
    public string Name { get; set; }
    public int RoleId {get;set;} 
    public string Password {get;set;} 
}
