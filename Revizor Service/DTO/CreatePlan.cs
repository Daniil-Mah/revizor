using System;

namespace Revizor_Service.DTO;

public class CreatePlan
{
    public string Name {get;set;}
    public DateOnly PeriodStart {get;set;}
    public DateOnly? PeriodEnd {get;set;}
}
