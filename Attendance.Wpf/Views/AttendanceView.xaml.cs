using System.Windows.Controls;
using Attendance.Wpf.ViewModels;

namespace Attendance.Wpf.Views;

public partial class AttendanceView : UserControl
{
    private readonly AttendanceViewModel _vm = new AttendanceViewModel();

    public AttendanceView()
    {
        InitializeComponent();
        DataContext = _vm;
    }
}

