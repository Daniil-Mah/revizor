namespace Revizor_Service.DTO;

public class ResultReportForId
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public List<ResultRecordForId> Resords {get;set;}
}

public class ResultRecordForId
{
    public DateTime CreatedAt {get;set;}
    public string  FacilityTitle { get; set; }
    public string  FacilityLocation { get; set; }
    public TimeOnly StartAt { get; set; }
    public TimeOnly EndAt { get; set; }
    public float PassegeWay {get;set;}
}
