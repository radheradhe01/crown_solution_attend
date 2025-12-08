using System;
using System.IO;
using Attendance.Core.Services;
using Xunit;

public class AuthServiceTests
{
    [Fact]
    public void CreatesAndValidatesUser()
    {
        var dbPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "attendance.db");
        var db = new Attendance.Core.Services.SqliteDb(dbPath);
        var auth = new AuthService(db);
        var user = auth.CreateUser("testuser", "Test User", "Password123!");
        var valid = auth.ValidateCredentials("testuser", "Password123!");
        Assert.NotNull(valid);
        Assert.Equal(user.Id, valid!.Id);
    }
}
