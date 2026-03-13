using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication2.Views;

public partial class TransportWindow : Window
{
    public decimal TransportCost { get; private set; } = 0;

    public TransportWindow()
    {
        InitializeComponent();
        var okBtn = this.FindControl<Button>("OkBtn");
        if (okBtn != null) okBtn.Click += OnOkClick;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void OnOkClick(object? sender, RoutedEventArgs e)
    {
        var inpost = this.FindControl<CheckBox>("InPostCheck");
        var courier = this.FindControl<CheckBox>("CourierCheck");
        var self = this.FindControl<CheckBox>("SelfCheck");

        if (inpost?.IsChecked == true) TransportCost += 15;
        if (courier?.IsChecked == true) TransportCost += 20;
        if (self?.IsChecked == true) TransportCost += 0;

        Close(true);
    }
}
