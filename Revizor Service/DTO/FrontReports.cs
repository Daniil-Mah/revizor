namespace Revizor_Service.DTO;
public class FrontReports
{
    public Guid PlanId {get;set;}
    public string Name { get; set; }
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }

}