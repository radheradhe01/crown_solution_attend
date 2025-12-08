using System.Collections.ObjectModel;
using Attendance.Core.Models;
using Attendance.Wpf.Services;

namespace Attendance.Wpf.ViewModels;

public class ReportsViewModel : BaseViewModel
{
    public ObservableCollection<AttendanceRecord> Records { get; } = new ObservableCollection<AttendanceRecord>();
    private DateTime? _from;
    public DateTime? From { get => _from; set => Set(ref _from, value); }
    private DateTime? _to;
    public DateTime? To { get => _to; set => Set(ref _to, value); }
    public int? UserId { get; set; }

    public void Generate()
    {
        Records.Clear();
        foreach (var r in AppServices.Attendance.GetRecords(UserId, From, To)) Records.Add(r);
    }

    public void Export(string filePath)
    {
        var list = Records.ToList();
        AppServices.Backup.RotateBackups();
        new Attendance.Core.Services.ExportService().ExportManualFormat(filePath, list, ResolveUsername, From, To);
    }

    private string ResolveUsername(int userId)
    {
        var u = AppServices.Users.GetAll().FirstOrDefault(x => x.Id == userId);
        return u?.Username ?? userId.ToString();
    }
}

