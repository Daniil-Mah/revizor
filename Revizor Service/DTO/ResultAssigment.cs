using Revizor_Service.Database.Models;

namespace Revizor_Service.DTO;

public class ResultAssigmentForRevizor
{
    public Guid Id { get; set; }
    public Guid FacilityId { get; set; }
    public string FacilityTitle { get; set; }
    public string FacilityLocation { get; set; }
    public DateTime PassAt { get; set; }
    public Guid RevizorId {get;set;}
    public User Revizor {get;set;}

    public ResponsiblePerson? ResponsiblePerson { get; set; }
}

public class ResultAssigmentForAdmin
{
    public Guid Id { get; set; }
    public Guid FacilityId { get; set; }
    public string FacilityTitle { get; set; }
    public string FacilityLocation { get; set; }
    public DateTime PassAt { get; set; }
    public List<Revizor> Revizors { get; set; }
    public ResponsiblePerson? ResponsiblePerson { get; set; }
}

public class Revizor
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
}
public class ResponsiblePerson
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
}
