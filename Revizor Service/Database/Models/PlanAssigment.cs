using System;

namespace Revizor_Service.Database.Models;

public class PlanAssigment
{
    public Guid Id {get;set;}
    public Guid PlanId {get;set;}
    public Guid FacilityId {get;set;}
    public DateTime PassAt {get;set;}
    public Guid PassBy {get;set;}
    public Guid ResponsiblePersonId {get;set;}
}

