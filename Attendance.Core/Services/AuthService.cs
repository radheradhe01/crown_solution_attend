using Attendance.Core.Models;
using Attendance.Core.Services.Security;
using Microsoft.Data.Sqlite;

namespace Attendance.Core.Services;

public class AuthService
{
    private readonly SqliteDb _db;

    public AuthService(SqliteDb db)
    {
        _db = db;
    }

    public User CreateUser(string username, string displayName, string password, string role = "Employee")
    {
        var salt = Crypto.GenerateSalt();
        var hash = Crypto.HashPassword(password, salt);
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO users(username, display_name, password_hash, password_salt, role, created_at, is_active) VALUES ($u,$d,$h,$s,$r,$c,$a); SELECT last_insert_rowid();";
        cmd.Parameters.AddWithValue("$u", username);
        cmd.Parameters.AddWithValue("$d", displayName);
        cmd.Parameters.AddWithValue("$h", hash);
        cmd.Parameters.AddWithValue("$s", salt);
        cmd.Parameters.AddWithValue("$r", role);
        cmd.Parameters.AddWithValue("$c", DateTime.UtcNow.ToString("o"));
        cmd.Parameters.AddWithValue("$a", 1);
        var id = Convert.ToInt32((long)cmd.ExecuteScalar()!);
        return new User { Id = id, Username = username, DisplayName = displayName, PasswordHash = hash, PasswordSalt = salt, Role = role, CreatedAt = DateTime.UtcNow, IsActive = true };
    }

    public User? ValidateCredentials(string username, string password)
    {
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, username, display_name, password_hash, password_salt, role, created_at, is_active FROM users WHERE username=$u AND is_active=1";
        cmd.Parameters.AddWithValue("$u", username);
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;
        var salt = reader.GetString(4);
        var hash = reader.GetString(3);
        if (!Crypto.VerifyPassword(password, salt, hash)) return null;
        return new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            DisplayName = reader.GetString(2),
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = reader.GetString(5),
            CreatedAt = DateTime.Parse(reader.GetString(6)),
            IsActive = reader.GetInt32(7) == 1
        };
    }

    public void SetActive(int userId, bool active)
    {
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE users SET is_active=$a WHERE id=$id";
        cmd.Parameters.AddWithValue("$a", active ? 1 : 0);
        cmd.Parameters.AddWithValue("$id", userId);
        cmd.ExecuteNonQuery();
    }

    public void ChangePassword(int userId, string newPassword)
    {
        var salt = Crypto.GenerateSalt();
        var hash = Crypto.HashPassword(newPassword, salt);
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE users SET password_hash=$h, password_salt=$s WHERE id=$id";
        cmd.Parameters.AddWithValue("$h", hash);
        cmd.Parameters.AddWithValue("$s", salt);
        cmd.Parameters.AddWithValue("$id", userId);
        cmd.ExecuteNonQuery();
    }
}

