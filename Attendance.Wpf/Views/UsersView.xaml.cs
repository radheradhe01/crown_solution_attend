using System.Windows.Controls;
using Attendance.Wpf.ViewModels;

namespace Attendance.Wpf.Views;

public partial class UsersView : UserControl
{
    private readonly UsersViewModel _vm = new UsersViewModel();

    public UsersView()
    {
        InitializeComponent();
        DataContext = _vm;
        Loaded += (_, __) => _vm.Load();
    }
}

