using System;

namespace Revizor_Service.DTO;

public class ResultPlanAssigment
{
    public string Facility { get; set; }
    public DateTime PassAt {get;set;}
    public Revizor[] Revizors { get; set; }
    public Revizor ResponsiblePerson {get;set;}
}
