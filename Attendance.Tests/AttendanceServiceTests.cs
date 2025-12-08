using System;
using System.IO;
using System.Linq;
using Attendance.Core.Services;
using Xunit;

public class AttendanceServiceTests
{
    [Fact]
    public void LoginThenLogoutCreatesRecord()
    {
        var dbPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "attendance.db");
        var db = new SqliteDb(dbPath);
        var auth = new AuthService(db);
        var svc = new AttendanceService(db);
        var user = auth.CreateUser("user1", "User One", "Secret123!");
        var record = svc.Login(user.Id);
        Assert.NotNull(record);
        svc.Logout(record.Id);
        var records = svc.GetRecords(user.Id).ToList();
        Assert.Single(records);
        Assert.NotNull(records[0].LogoutAt);
    }
}
