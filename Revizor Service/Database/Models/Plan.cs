
namespace Revizor_Service.Database.Models;

public class Plan
{
    public Guid Id  {get;set;}
    public string Name {get;set;}
    public DateTime CreatedAt {get;set;}
    public Guid CreatedById {get;set;}
    public User CreatedBy {get;set;}

    public DateTime PeriodStart {get;set;}
    public DateTime? PeriodEnd {get;set;}
    public bool IsArchived {get;set;}
}
