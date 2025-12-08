using Attendance.Core.Models;
using Microsoft.Data.Sqlite;

namespace Attendance.Core.Services;

public class AttendanceService
{
    private readonly SqliteDb _db;

    public AttendanceService(SqliteDb db)
    {
        _db = db;
    }

    public AttendanceRecord Login(int userId, string? notes = null)
    {
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO attendance_records(user_id, login_at, notes) VALUES ($u,$l,$n); SELECT last_insert_rowid();";
        cmd.Parameters.AddWithValue("$u", userId);
        cmd.Parameters.AddWithValue("$l", DateTime.UtcNow.ToString("o"));
        cmd.Parameters.AddWithValue("$n", notes ?? (object)DBNull.Value);
        var id = Convert.ToInt32((long)cmd.ExecuteScalar()!);
        return new AttendanceRecord { Id = id, UserId = userId, LoginAt = DateTime.UtcNow, Notes = notes };
    }

    public void Logout(int recordId)
    {
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE attendance_records SET logout_at=$o WHERE id=$id AND logout_at IS NULL";
        cmd.Parameters.AddWithValue("$o", DateTime.UtcNow.ToString("o"));
        cmd.Parameters.AddWithValue("$id", recordId);
        cmd.ExecuteNonQuery();
    }

    public IEnumerable<AttendanceRecord> GetRecords(int? userId = null, DateTime? from = null, DateTime? to = null)
    {
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        var where = new List<string>();
        if (userId.HasValue) where.Add("user_id=$u");
        if (from.HasValue) where.Add("login_at>= $f");
        if (to.HasValue) where.Add("login_at<= $t");
        var sql = "SELECT id, user_id, login_at, logout_at, notes FROM attendance_records" + (where.Count > 0 ? " WHERE " + string.Join(" AND ", where) : "") + " ORDER BY login_at";
        cmd.CommandText = sql;
        if (userId.HasValue) cmd.Parameters.AddWithValue("$u", userId.Value);
        if (from.HasValue) cmd.Parameters.AddWithValue("$f", from.Value.ToString("o"));
        if (to.HasValue) cmd.Parameters.AddWithValue("$t", to.Value.ToString("o"));
        using var reader = cmd.ExecuteReader();
        var list = new List<AttendanceRecord>();
        while (reader.Read())
        {
            list.Add(new AttendanceRecord
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                LoginAt = DateTime.Parse(reader.GetString(2)),
                LogoutAt = reader.IsDBNull(3) ? null : DateTime.Parse(reader.GetString(3)),
                Notes = reader.IsDBNull(4) ? null : reader.GetString(4)
            });
        }
        return list;
    }
}

