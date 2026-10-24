
namespace Revizor_Service.Database.Models;

public class Plan
{
    public Guid Id  {get;set;}
    public string Name {get;set;}
    public DateTime CreatedAt {get;set;}
    public Guid CreatedById {get;set;}
    public User CreatedBy {get;set;}

    public DateOnly PeriodStart {get;set;}
    public DateOnly? PeriodEnd {get;set;}
    public bool IsArchived {get;set;}
}
