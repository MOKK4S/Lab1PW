using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.ViewModels;

namespace AvaloniaApplication2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        
        if (DataContext is MainWindowViewModel vm)
        {
            vm.RequestProductSelection = OpenProductSelection;
            vm.RequestTransportSelection = OpenTransportSelection;
            vm.RequestPaymentSelection = OpenPaymentSelection;
        }
    }

    public async void OpenPaymentSelection()
    {
        if (DataContext is MainWindowViewModel mainVm)
        {
            var dialog = new PaymentWindow(mainVm.TotalPrice);
            var result = await dialog.ShowDialog<bool>(this);
            if (result)
            {
                mainVm.Cart.Clear();
                mainVm.TotalPrice = 0;
            }
        }
    }

    public async void OpenTransportSelection()
    {
        var dialog = new TransportWindow();
        await dialog.ShowDialog<bool>(this);
        
        if (DataContext is MainWindowViewModel mainVm)
        {
            mainVm.TotalPrice += dialog.TransportCost;
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public async void OpenProductSelection()
    {
        var dialog = new ProductSelectionWindow();
        var result = await dialog.ShowDialog<bool>(this);

        if (result && dialog.DataContext is ProductSelectionViewModel vm && vm.SelectedProduct != null)
        {
            if (DataContext is MainWindowViewModel mainVm)
            {
                mainVm.Cart.Add(vm.SelectedProduct);
                mainVm.TotalPrice += vm.SelectedProduct.Price;
            }
        }
    }
}
