using Revizor_Service.Database;
using ClosedXML.Excel;
using Revizor_Service.Database.Models;
using Microsoft.EntityFrameworkCore;
using Revizor_Service.DTO;

namespace Revizor_Service.Services;
public class ReportsServices
{

    private readonly string _folderReports;
    private readonly AppDbContext _db;

    public ReportsServices(IConfiguration cfg, AppDbContext db) 
    {
        _db = db;
        _folderReports = cfg["folderReports"]!;
    }

    public async Task<List<ResultReportForId>> GetRecordsByPlan(Guid planId)
    {
        var plan = await _db.Plans.FirstOrDefaultAsync(p=> p.Id == planId);
        if (plan == null) return [];

        var assignments = await _db.PlanAssigments
            .Where(a => a.PlanId == planId)
            .Include(a => a.PassBy)
            .Include(a => a.Facility)
            .ToListAsync();

        var records = await _db.Records
            .Where(r => r.PlanAssigment.PlanId == planId)
            .ToListAsync();

        var result = assignments
            .Where(a => records.Any(r => r.PlanAssigmentId == a.Id))
            .GroupBy(a => a.PassBy)
            .Select(g => new ResultReportForId
            {
                Id = g.Key.Id,
                FullName = g.Key.GetFullName(),
                Resords = g.Select(a =>
                {
                    var record = records.FirstOrDefault(r => r.PlanAssigmentId == a.Id);
                    return new ResultRecordForId
                    {
                        FacilityTitle = a.Facility.Title,
                        FacilityLocation = a.Facility.Location,
                        StartAt = record != null 
                            ? TimeOnly.FromDateTime(record.PlanAssigment.PassAt) 
                            : TimeOnly.MinValue,
                        EndAt = record?.EndAt ?? TimeOnly.MinValue,
                        PassegeWay = record?.PassageWay ?? 0f
                    };
                }).ToList()
            })
            .ToList();
        return result;
    }
    
    // блядская формула для вычисления ночной смены 
    public  TimeSpan GetNightHours(TimeOnly start, TimeOnly end)
    {
        var startTs = start.ToTimeSpan();
        var endTs = end.ToTimeSpan();
        
        if (endTs < startTs)
            endTs = endTs.Add(TimeSpan.FromHours(24));
        
        var night1Start = TimeSpan.Zero;
        var night1End = TimeSpan.FromHours(6);
        var night2Start = TimeSpan.FromHours(22);
        var night2End = TimeSpan.FromHours(30);
        
        var total = TimeSpan.Zero;
        
        var p1Start = startTs > night1Start ? startTs : night1Start;
        var p1End = endTs < night1End ? endTs : night1End;
        if (p1End > p1Start)
            total += p1End - p1Start;
        
        var p2Start = startTs > night2Start ? startTs : night2Start;
        var p2End = endTs < night2End ? endTs : night2End;
        if (p2End > p2Start)
            total += p2End - p2Start;
        
        return total;
    }

    public async Task<int> CreateDocumentReportAdmin(Guid planId)
    {
        using var workbook = new XLWorkbook();
        var currentDate = DateTime.Now;
        var records = await GetRecordsByPlan(planId);
        if (records.Count == 0) return 0;
        foreach (var user in records)
        {
            var worksheet = workbook.Worksheets.Add($"{user.FullName}");
            worksheet.Cell("A1").Value = currentDate.ToString("dd.MM.yyyy");

            worksheet.Range("A2:G2").Merge();
            worksheet.Cell("A2").Value = user.FullName.ToString();
            worksheet.Cell("A3").Value = "Наименование объекта";
            worksheet.Cell("B3").Value = "Время";
            worksheet.Cell("C3").Value = "Общее";
            worksheet.Cell("D3").Value = "Ночные";
            worksheet.Cell("E3").Value = "Проезд";

            for (var i = 0; i < user.Resords.Count; i++)
            {
                worksheet.Cell($"A{i + 4}").Value = $"{user.Resords[i].FacilityTitle} {user.Resords[i].FacilityLocation}";
                worksheet.Cell($"B{i + 4}").Value = $"{user.Resords[i].StartAt.ToString("HH:mm:ss")} - {user.Resords[i].EndAt.ToString("HH:mm:ss")}";
                worksheet.Cell($"C{i + 4}").Value = (user.Resords[i].EndAt.ToTimeSpan() - user.Resords[i].StartAt.ToTimeSpan()).ToString(@"hh\:mm\:ss");
                var startAt = user.Resords[i].StartAt;
                var endAt = user.Resords[i].EndAt;

                worksheet.Cell($"D{i + 4}").Value = GetNightHours(startAt, endAt);
                worksheet.Cell($"E{i + 4}").Value = user.Resords[i].PassegeWay;
                await _db.Reports.AddAsync(new Report
                {
                    PlanId = planId,
                    UserId = user.Id,
                    FileName = $"user_{user.FullName}",
                    FilePath = $"{_folderReports}/Revizor/",
                    CreatedAt = DateTime.UtcNow
                });
                await _db.SaveChangesAsync();
            }
        }
        workbook.SaveAs($"{_folderReports}/Admin/{currentDate.Month}_{currentDate.Year}.xlsx");
        return 1;
    }   

    public async Task<int> CreateDocumentReportUser(Guid reportId, string userName)
    {
        // using (var workbook = new XLWorkbook())
        // {
        //     var currentDate = DateTime.Now;
        //     var worksheet = workbook.AddWorksheet(" ");
        //     var report = await _db.Reports.FirstOrDefaultAsync(r=> r.Id == reportId);
            
        //     if (report == null) return 0;
        //     var records = await _db.Records.Where(r=> r.PlanId == report.PlanId).ToArrayAsync();

        //     worksheet.Range("A2:G2").Merge();
        //     worksheet.Cell("A2").Value =  userName;
        //     worksheet.Cell("A3").Value = "Дата";
        //     worksheet.Cell("B3").Value = "Наименование объекта";
        //     worksheet.Cell("C3").Value = "Время";
        //     worksheet.Cell("D3").Value = "Общее";
        //     worksheet.Cell("E3").Value = "Ночные";
        //     worksheet.Cell("F3").Value = "Проезд";
        //     for (int i = 0 ;i<records.Length; i++)
        //     {
        //         worksheet.Cell($"A{i+4}").Value = records[i].CreatedAt.ToString("dd.MM.yyyy HH:mm:ss");
        //         worksheet.Cell($"B{i+4}").Value = $"{records[i].Facility.Title} {records[i].Facility.Location}";
        //         worksheet.Cell($"C{i+4}").Value = $"{records[i].StartAt.ToString("HH:mm:ss")} - {records[i].EndAt.ToString("HH:mm:ss")}";
        //         worksheet.Cell($"D{i+4}").Value = (records[i].EndAt.ToTimeSpan() - records[i].StartAt.ToTimeSpan()).ToString(@"hh\:mm\:ss");
                
        //         var startAt = records[i].StartAt;
        //         var endAt = records[i].EndAt;

        //         worksheet.Cell($"E{i+4}").Value = GetNightHours(startAt,endAt);
        //         worksheet.Cell($"F{i+4}").Value = records[i].PassageWay;
        //     }
            
        //     workbook.SaveAs($"{_folderReports}/Revizor/{currentDate.Month}_{currentDate.Year}.xlsx");
        //     return 1;
        // }
        return 1;
    }
}