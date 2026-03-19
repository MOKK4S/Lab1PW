using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lab3PW.ViewModels;
using Lab3PW.Models;
namespace Lab3PW.Views;
public partial class AddEmployeeWindow : Window
{
    public AddEmployeeWindow()
    {
        InitializeComponent();
        var vm = new AddEmployeeViewModel();
        vm.RequestClose = (employee) => Close(employee);
        DataContext = vm;
    }
    private void InitializeComponent() { AvaloniaXamlLoader.Load(this); }
}
