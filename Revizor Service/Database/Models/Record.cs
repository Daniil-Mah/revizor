using System;

namespace Revizor_Service.Database.Models;

public class Record
{
    public Guid Id {get;set;}
    public Guid PlanId {get;set;}
    public Plan Plan {get;set;}
    public DateTime CreatedAt { get; set; }
    public Guid UserId {get;set;}
    public User User {get;set;}
    public Guid FacilityId {get;set;}
    public Facility Facility {get;set;}
    public TimeOnly StartAt {get;set;}
    public TimeOnly EndAt {get;set;}
    public float? PassageWay {get;set;}
}
