using System;

namespace Revizor_Service.Database.Models;

public class Record
{
    public Guid Id {get;set;}
    public Guid PlanAssigmentId {get;set;}
    public PlanAssigment PlanAssigment {get;set;}
    public TimeOnly EndAt {get;set;}
    public float? PassageWay {get;set;}
}
