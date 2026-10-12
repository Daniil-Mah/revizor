using System;
using Revizor_Service.Database.Models;

namespace Revizor_Service.DTO;

public class PatchUser
{
    public string Name { get; set; }
    public Role Role {get;set;} 
    public string Password {get;set;} 
}
