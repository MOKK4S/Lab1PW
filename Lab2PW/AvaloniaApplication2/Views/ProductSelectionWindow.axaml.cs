using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication2.ViewModels;

namespace AvaloniaApplication2.Views;

public partial class ProductSelectionWindow : Window
{
    public ProductSelectionWindow()
    {
        InitializeComponent();
        DataContext = new ProductSelectionViewModel();
    }

    private void OnAddClick(object? sender, RoutedEventArgs e)
    {
        Close(true); // Zwracamy 'true' jako wynik, jeśli użytkownik kliknął 'Dodaj'
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(false); // Zwracamy 'false', jeśli użytkownik anulował
    }
}
