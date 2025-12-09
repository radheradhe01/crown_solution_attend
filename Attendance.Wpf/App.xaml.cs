using System.Windows;
using Attendance.Wpf.Services;
using System.Linq;

namespace Attendance.Wpf;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        if (!AppServices.Users.GetAll().Any())
        {
            AppServices.Auth.CreateUser("admin", "Administrator", "admin123", "Admin");
        }
    }
}

