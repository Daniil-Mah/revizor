namespace Revizor_Service.DTO;

public class PatchPlan
{
    public string? Name {get; set;} = null;
    public DateTime? CreatedAt {get; set;} = null; 
    public Guid? CreatedBy {get; set;} = null; 
    public DateOnly? PeriodStart {get; set;} = null;
    public DateOnly? PeriodEnd {get; set;} = null;
}
