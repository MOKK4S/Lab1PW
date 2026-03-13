using System.Collections.ObjectModel;
using AvaloniaApplication2.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private decimal _totalPrice;

    public ObservableCollection<Product> Cart { get; } = new();

    public System.Action? RequestProductSelection { get; set; }
    public System.Action? RequestTransportSelection { get; set; }
    public System.Action? RequestPaymentSelection { get; set; }

    [RelayCommand]
    private void OpenProductSelection()
    {
        RequestProductSelection?.Invoke();
    }

    [RelayCommand]
    private void OpenTransportSelection()
    {
        RequestTransportSelection?.Invoke();
    }

    [RelayCommand]
    private void OpenPaymentSelection()
    {
        RequestPaymentSelection?.Invoke();
    }

    [RelayCommand]
    private void ClearCart()
    {
        Cart.Clear();
        TotalPrice = 0;
    }
}
