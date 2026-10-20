
namespace Revizor_Service.DTO;

public class PatchRecord
{
    public Guid? Id {get;set;}
    public Guid? PlanAssigmentId {get;set;}
    public TimeOnly? EndAt {get;set;}
    public float? PassageWay {get;set;}
}