using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MathLibrary;

namespace Lab1PW.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _number1 = "0";

    [ObservableProperty]
    private string _number2 = "0";

    [ObservableProperty]
    private string _result = "0";

    [RelayCommand]
    private void Add() => Calculate(MathOperations.Add);

    [RelayCommand]
    private void Subtract() => Calculate(MathOperations.Subtract);

    [RelayCommand]
    private void Multiply() => Calculate(MathOperations.Multiply);

    [RelayCommand]
    private void Divide() => Calculate(MathOperations.Divide);

    private void Calculate(System.Func<double, double, double> op)
    {
        if (double.TryParse(Number1, out double a) && double.TryParse(Number2, out double b))
        {
            Result = op(a, b).ToString();
        }
        else
        {
            Result = "Error!";
        }
    }
}
