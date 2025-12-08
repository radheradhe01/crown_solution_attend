using Attendance.Core.Models;
using ClosedXML.Excel;

namespace Attendance.Core.Services;

public class ExportService
{
    public void ExportManualFormat(string filePath, IEnumerable<AttendanceRecord> records, Func<int, string> usernameResolver, DateTime? from = null, DateTime? to = null)
    {
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Attendance");
        ws.Cell(1, 1).Value = "Date";
        ws.Cell(1, 2).Value = "User";
        ws.Cell(1, 3).Value = "Login Time";
        ws.Cell(1, 4).Value = "Logout Time";
        ws.Cell(1, 5).Value = "Total Hours";
        var row = 2;
        foreach (var r in records)
        {
            var date = r.LoginAt.ToLocalTime().Date;
            if (from.HasValue && date < from.Value.Date) continue;
            if (to.HasValue && date > to.Value.Date) continue;
            ws.Cell(row, 1).Value = date;
            ws.Cell(row, 2).Value = usernameResolver(r.UserId);
            ws.Cell(row, 3).Value = r.LoginAt.ToLocalTime();
            ws.Cell(row, 4).Value = r.LogoutAt?.ToLocalTime();
            var total = r.LogoutAt.HasValue ? (r.LogoutAt.Value - r.LoginAt).TotalHours : 0;
            ws.Cell(row, 5).Value = Math.Round(total, 2);
            row++;
        }
        ws.Columns().AdjustToContents();
        wb.SaveAs(filePath);
    }
}

