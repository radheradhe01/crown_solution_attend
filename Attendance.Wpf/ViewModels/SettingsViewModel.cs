using Attendance.Wpf.Services;

namespace Attendance.Wpf.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private string _backupFolder = string.Empty;
    public string BackupFolder { get => _backupFolder; set => Set(ref _backupFolder, value); }

    public string CreateBackup()
    {
        var path = AppServices.Backup.CreateBackup();
        AppServices.Backup.RotateBackups();
        return path;
    }
}

