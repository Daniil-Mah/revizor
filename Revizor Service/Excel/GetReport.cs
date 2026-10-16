using ClosedXML.Excel;

public class Reports()
{
    public static void GetReports()
    {
        using (var workbook = new XLWorkbook())
        {
            var currentDate = DateTime.Now;
            var worksheet = workbook.Worksheets.Add($"{currentDate.ToString("dd.MM.yyyy HH-mm-ss")}");
            worksheet.Cell("A1").Value = "Hello World!";
            worksheet.Cell("A2").FormulaA1 = "=MID(A1, 7, 5)";
            workbook.SaveAs($"./Excel/Reports/{currentDate.Month}_{currentDate.Year}.xlsx");
        }
    }   
}