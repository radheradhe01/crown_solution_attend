using System.Collections.ObjectModel;
using Attendance.Core.Models;
using Attendance.Wpf.Services;

namespace Attendance.Wpf.ViewModels;

public class UsersViewModel : BaseViewModel
{
    public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

    public void Load()
    {
        Users.Clear();
        foreach (var u in AppServices.Users.GetAll()) Users.Add(u);
    }

    public void Add(string username, string displayName, string password, string role)
    {
        AppServices.Auth.CreateUser(username, displayName, password, role);
        Load();
    }

    public void Update(User user)
    {
        AppServices.Users.Update(user);
        Load();
    }

    public void Remove(int userId)
    {
        AppServices.Users.Delete(userId);
        Load();
    }
}

