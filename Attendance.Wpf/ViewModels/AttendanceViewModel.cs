using System.Collections.ObjectModel;
using Attendance.Core.Models;
using Attendance.Wpf.Services;

namespace Attendance.Wpf.ViewModels;

public class AttendanceViewModel : BaseViewModel
{
    public ObservableCollection<AttendanceRecord> Records { get; } = new ObservableCollection<AttendanceRecord>();
    private AttendanceRecord? _activeRecord;
    public AttendanceRecord? ActiveRecord { get => _activeRecord; set => Set(ref _activeRecord, value); }

    public void Load(int? userId = null, DateTime? from = null, DateTime? to = null)
    {
        Records.Clear();
        foreach (var r in AppServices.Attendance.GetRecords(userId, from, to)) Records.Add(r);
    }

    public void Login(int userId, string? notes = null)
    {
        ActiveRecord = AppServices.Attendance.Login(userId, notes);
        Load(userId);
    }

    public void Logout()
    {
        if (ActiveRecord == null) return;
        AppServices.Attendance.Logout(ActiveRecord.Id);
        Load(ActiveRecord.UserId);
        ActiveRecord = null;
    }
}

