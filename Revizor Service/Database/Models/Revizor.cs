using System;

namespace Revizor_Service.Database.Models;

public class Revizor
{
    public Guid Id {get;set;}
    public string Name { get; set; }
    public Role Role {get;set;} 
    public string Password {get;set;} 
    public DateTime CreatedAt {get;set;}
}
