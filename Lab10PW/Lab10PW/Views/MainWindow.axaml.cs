using Avalonia.Controls;
using Lab10PW.ViewModels;

namespace Lab10PW.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnOpened(System.EventArgs e)
    {
        base.OnOpened(e);
        if (DataContext is MainViewModel vm)
            vm.StorageProvider = TopLevel.GetTopLevel(this)?.StorageProvider;
    }
}
