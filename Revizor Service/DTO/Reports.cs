using System;
using Revizor_Service.Database.Models;

namespace Revizor_Service.DTO;

public class ResultReports
{
    public Guid Id { get; set; }
    public string PlanName { get; set; }
    public List<ReportRecord> Records { get; set; }
}

public class ReportRecord
{
    public ReportUser User { get; set; }
    public List<Record> Record { get; set; }
}

public class ReportUser
{
    public Guid Id { get; set; }
    public string FullName { get; set; }

}