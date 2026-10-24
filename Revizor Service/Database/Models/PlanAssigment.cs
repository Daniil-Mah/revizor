using System;

namespace Revizor_Service.Database.Models;

public class PlanAssigment
{
    public Guid Id {get;set;}
    public Guid PlanId {get;set;}
    public Plan? Plan {get;set;}
    public Guid FacilityId {get;set;}
    public Facility? Facility {get;set;}
    public DateTime PassAt {get;set;}
    public Guid PassById {get;set;}
    public User? PassBy {get;set;}
    public Guid ResponsiblePersonId {get;set;}
    public User? ResponsiblePerson {get;set;}
    public StatusPlanAssigment Status {get;set;}
}

public enum StatusPlanAssigment
{
    Done,
    InProgres,
    Futures
}
