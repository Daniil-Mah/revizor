using System;

namespace Revizor_Service.DTO;

public class CreateRecord
{
    public Guid FacilityId {get;set;}
    public TimeOnly StartAt {get;set;}
    public TimeOnly EndAt {get;set;}
    public float PassageWay {get;set;}
    public Guid PlanId {get;set;}
}
