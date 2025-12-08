using Attendance.Core.Models;
using Attendance.Wpf.Services;

namespace Attendance.Wpf.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private string _username = string.Empty;
    public string Username { get => _username; set => Set(ref _username, value); }

    private User? _currentUser;
    public User? CurrentUser { get => _currentUser; set => Set(ref _currentUser, value); }

    public bool TryLogin(string password)
    {
        var user = AppServices.Auth.ValidateCredentials(Username, password);
        CurrentUser = user;
        return user != null;
    }
}

