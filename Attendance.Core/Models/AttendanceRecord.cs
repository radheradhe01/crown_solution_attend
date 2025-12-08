namespace Attendance.Core.Models;

public class AttendanceRecord
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime LoginAt { get; set; }
    public DateTime? LogoutAt { get; set; }
    public string? Notes { get; set; }
}

