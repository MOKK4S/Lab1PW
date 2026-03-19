using System;
using System.Collections.ObjectModel;
using Lab3PW.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Lab3PW.ViewModels;

public partial class AddEmployeeViewModel : ViewModelBase
{
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private int _age = 20;
    [ObservableProperty] private string? _selectedPosition;

    public ObservableCollection<string> Positions { get; } = new()
    {
        "Programista", "Tester", "Projektant", "Menedżer", "HR", "Księgowy"
    };

    public Action<Employee?>? RequestClose { get; set; }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || SelectedPosition == null) return;
        var employee = new Employee { FirstName = FirstName, LastName = LastName, Age = Age, Position = SelectedPosition };
        RequestClose?.Invoke(employee);
    }

    [RelayCommand]
    private void Cancel() => RequestClose?.Invoke(null);
}
