using System;
using System.Collections.Generic;
using System.IO;
using Attendance.Core.Models;
using Attendance.Core.Services;
using Xunit;

public class ExportServiceTests
{
    [Fact]
    public void ExportsExcelFile()
    {
        var exportPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "export.xlsx");
        Directory.CreateDirectory(Path.GetDirectoryName(exportPath)!);
        var records = new List<AttendanceRecord>
        {
            new AttendanceRecord { Id = 1, UserId = 1, LoginAt = DateTime.UtcNow.AddHours(-8), LogoutAt = DateTime.UtcNow },
            new AttendanceRecord { Id = 2, UserId = 2, LoginAt = DateTime.UtcNow.AddHours(-7), LogoutAt = DateTime.UtcNow }
        };
        new ExportService().ExportManualFormat(exportPath, records, id => $"user{id}");
        Assert.True(File.Exists(exportPath));
    }
}
