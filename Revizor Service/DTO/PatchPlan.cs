using System;

namespace Revizor_Service.DTO;

public class PatchPlan
{
    public DateTime? PassAt { get; set; }    
    public Guid? Facility {get;set;}
    public Guid? PassBy {get;set;}
    public Guid? ResponsiblePrson {get;set;}
    public bool? IsArchived {get;set;}
}
