using System.IO.Compression;

namespace Attendance.Core.Services;

public class BackupService
{
    private readonly string _dbPath;
    private readonly string _backupDir;

    public BackupService(string dbPath, string backupDir)
    {
        _dbPath = dbPath;
        _backupDir = backupDir;
        Directory.CreateDirectory(_backupDir);
    }

    public string CreateBackup()
    {
        var stamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        var zipPath = Path.Combine(_backupDir, $"attendance_{stamp}.zip");
        using var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create);
        zip.CreateEntryFromFile(_dbPath, Path.GetFileName(_dbPath));
        return zipPath;
    }

    public void RotateBackups(int keep = 30)
    {
        var files = new DirectoryInfo(_backupDir).GetFiles("attendance_*.zip").OrderByDescending(f => f.CreationTimeUtc).ToList();
        foreach (var f in files.Skip(keep)) f.Delete();
    }
}

