using System.Windows.Controls;
using Attendance.Wpf.ViewModels;

namespace Attendance.Wpf.Views;

public partial class SettingsView : UserControl
{
    private readonly SettingsViewModel _vm = new SettingsViewModel();

    public SettingsView()
    {
        InitializeComponent();
        DataContext = _vm;
    }
}

