using Revizor_Service.Database;
using ClosedXML.Excel;
using Revizor_Service.Database.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Dictionary<Guid, List<Record>>> GetRecordsByPlan(Guid planId)
    {
        var results = new Dictionary<Guid, List<Record>>();
        var plan = await _db.Plans.Where(p=> p.Id == planId).FirstOrDefaultAsync();
        if (plan == null) {return results;}
        var records = await _db.Records.Where(r=> r.PlanId == plan.Id).OrderBy(r=> r.UserId).ToArrayAsync();
        foreach (var record in records)
        {
            if (results.ContainsKey(record.UserId))
            {
                results[record.UserId].Add(record);
            }
            else
            {
                results.Add(record.UserId, new List<Record>() {record});
            }
        }
        return results;
    }

    public async Task CreateDocumentReport(Guid planId)
    {
        using (var workbook = new XLWorkbook())
        {
            var currentDate = DateTime.Now;
            var records = await GetRecordsByPlan(planId);
            foreach(var user in records)
            {
                var worksheet = workbook.Worksheets.Add($"{user.Key}");
                worksheet.Cell("A1").Value = currentDate.ToString("dd.MM.yyyy"); 

                worksheet.Range("A2:G2").Merge();
                worksheet.Cell("A2").Value = user.Key.ToString();
                worksheet.Cell("A3").Value = "Дата";
                worksheet.Cell("B3").Value = "Наименование объекта";
                worksheet.Cell("C3").Value = "Время";
                worksheet.Cell("D3").Value = "Общее";
                worksheet.Cell("E3").Value = "Ночные";
                worksheet.Cell("F3").Value = "Проезд";

                for (var i = 0; i < user.Value.Count; i++)
                {
                    worksheet.Cell($"A{i+4}").Value = user.Value[i].CreatedAt.ToString("dd.MM.yyyy HH:mm:ss");
                    worksheet.Cell($"B{i+4}").Value = $"{user.Value[i].Facility.Title} {user.Value[i].Facility.Location}";
                    worksheet.Cell($"C{i+4}").Value = $"{user.Value[i].StartAt.ToString("HH:mm:ss")} - {user.Value[i].EndAt.ToString("HH:mm:ss")}";
                    worksheet.Cell($"D{i+4}").Value = (user.Value[i].EndAt - user.Value[i].StartAt).ToString("hh:mm:ss");
                    // блядская формула для вычисления ночной смены 
                    worksheet.Cell($"E{i+4}").FormulaA1="=MAX(0; MIN(B2+(B2<A2); 6/24) - MAX(A2; 0/24)) + MAX(0; МИН(B2+(B2<A2); 30/24) - MAX(A2; 22/24))";
                    worksheet.Cell($"F{i+4}").Value = user.Value[i].PassageWay;
                }
                await _db.Reports.AddAsync(new Report {PlanId = planId, UserId = user.Key,FileName = $"user_{user.Key}",FilePath=$"{_folderReports}/user/"});
                await _db.SaveChangesAsync();
            }
            workbook.SaveAs($"{_folderReports}/ADMIN_{currentDate.Month}_{currentDate.Year}.xlsx");
        }
    }   
}