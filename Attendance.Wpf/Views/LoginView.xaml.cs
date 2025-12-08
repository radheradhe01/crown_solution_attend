using System.Windows.Controls;
using Attendance.Wpf.ViewModels;

namespace Attendance.Wpf.Views;

public partial class LoginView : UserControl
{
    private readonly LoginViewModel _vm = new LoginViewModel();

    public LoginView()
    {
        InitializeComponent();
        DataContext = _vm;
    }

    private void OnLogin(object sender, System.Windows.RoutedEventArgs e)
    {
        var success = _vm.TryLogin(Password.Password);
        if (!success) System.Windows.MessageBox.Show("Invalid credentials");
    }
}

