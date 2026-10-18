using System;

namespace Revizor_Service.Database.Models;

public class Report
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PlanId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
}
