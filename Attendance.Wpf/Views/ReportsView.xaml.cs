using System.Windows.Controls;
using Microsoft.Win32;
using Attendance.Wpf.ViewModels;

namespace Attendance.Wpf.Views;

public partial class ReportsView : UserControl
{
    private readonly ReportsViewModel _vm = new ReportsViewModel();

    public ReportsView()
    {
        InitializeComponent();
        DataContext = _vm;
    }

    private void OnGenerate(object sender, System.Windows.RoutedEventArgs e)
    {
        _vm.Generate();
    }

    private void OnExport(object sender, System.Windows.RoutedEventArgs e)
    {
        var dlg = new SaveFileDialog { Filter = "Excel Workbook (*.xlsx)|*.xlsx" };
        if (dlg.ShowDialog() == true) _vm.Export(dlg.FileName);
    }
}

