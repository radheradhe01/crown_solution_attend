using Attendance.Core.Services;

namespace Attendance.Wpf.Services;

public static class AppServices
{
    private static readonly string AppDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AttendanceApp");
    private static readonly string DbPath = Path.Combine(AppDir, "attendance.db");
    private static readonly string BackupDir = Path.Combine(AppDir, "backups");

    public static SqliteDb Db { get; } = new SqliteDb(DbPath);
    public static AuthService Auth { get; } = new AuthService(Db);
    public static UsersRepository Users { get; } = new UsersRepository(Db);
    public static AttendanceService Attendance { get; } = new AttendanceService(Db);
    public static BackupService Backup { get; } = new BackupService(DbPath, BackupDir);
}

