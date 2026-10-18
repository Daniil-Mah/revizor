using System;

namespace Revizor_Service.DTO;

public class CreatePlan
{
    public string Name {get;set;}
    public DateTime PeriodStart {get;set;}
    public DateTime? PeriodEnd {get;set;} = DateTime.UtcNow.AddMonths(1);
}
