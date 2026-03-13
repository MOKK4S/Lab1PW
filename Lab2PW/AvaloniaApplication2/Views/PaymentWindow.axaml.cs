using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication2.Views;

public partial class PaymentWindow : Window
{
    public PaymentWindow() : this(0) { }

    public PaymentWindow(decimal amountToPay)
    {
        InitializeComponent();
        var text = this.FindControl<TextBlock>("AmountText");
        if (text != null) text.Text = $"Kwota: {amountToPay:C}";

        var payBtn = this.FindControl<Button>("PayBtn");
        if (payBtn != null) payBtn.Click += (s, e) => Close(true);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
