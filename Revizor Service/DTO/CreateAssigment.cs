using Revizor_Service.Database.Models;

namespace Revizor_Service.DTO;

public class CreateAssigment
{
    public Guid FacilityId {get;set;}
    public DateTime PassAt {get;set;}
    public Guid PassById {get;set;}
    public Guid ResponsiblePersonId {get;set;}
}
