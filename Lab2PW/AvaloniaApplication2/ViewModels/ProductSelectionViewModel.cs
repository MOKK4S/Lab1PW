using System;
using System.Collections.ObjectModel;
using AvaloniaApplication2.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication2.ViewModels;

public partial class ProductSelectionViewModel : ViewModelBase
{
    public ObservableCollection<Product> AvailableProducts { get; } = new()
    {
        new Product { Name = "Kebab Kurier (Mały)", Price = 18.00m },
        new Product { Name = "Kebab Wołowy (Średni)", Price = 24.50m },
        new Product { Name = "Kebab Mieszany (Duży)", Price = 29.00m },
        new Product { Name = "Kebab Rollo Kurczak", Price = 22.00m },
        new Product { Name = "Kebab Box Mieszany", Price = 26.00m },
        new Product { Name = "Frytki Duże", Price = 12.00m },
        new Product { Name = "Napój Gazowany", Price = 7.50m }
    };

    [ObservableProperty]
    private Product? _selectedProduct;

    public Action<bool>? RequestClose { get; set; }

    [RelayCommand]
    private void Add()
    {
        RequestClose?.Invoke(true);
    }

    [RelayCommand]
    private void Cancel()
    {
        RequestClose?.Invoke(false);
    }
}
