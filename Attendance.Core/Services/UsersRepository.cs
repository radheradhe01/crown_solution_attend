using Attendance.Core.Models;

namespace Attendance.Core.Services;

public class UsersRepository
{
    private readonly SqliteDb _db;

    public UsersRepository(SqliteDb db)
    {
        _db = db;
    }

    public IEnumerable<User> GetAll()
    {
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, username, display_name, password_hash, password_salt, role, created_at, is_active FROM users ORDER BY username";
        using var reader = cmd.ExecuteReader();
        var list = new List<User>();
        while (reader.Read())
        {
            list.Add(new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                DisplayName = reader.GetString(2),
                PasswordHash = reader.GetString(3),
                PasswordSalt = reader.GetString(4),
                Role = reader.GetString(5),
                CreatedAt = DateTime.Parse(reader.GetString(6)),
                IsActive = reader.GetInt32(7) == 1
            });
        }
        return list;
    }

    public void Update(User user)
    {
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE users SET username=$u, display_name=$d, role=$r, is_active=$a WHERE id=$id";
        cmd.Parameters.AddWithValue("$u", user.Username);
        cmd.Parameters.AddWithValue("$d", user.DisplayName);
        cmd.Parameters.AddWithValue("$r", user.Role);
        cmd.Parameters.AddWithValue("$a", user.IsActive ? 1 : 0);
        cmd.Parameters.AddWithValue("$id", user.Id);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int userId)
    {
        using var conn = _db.GetConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM users WHERE id=$id";
        cmd.Parameters.AddWithValue("$id", userId);
        cmd.ExecuteNonQuery();
    }
}

