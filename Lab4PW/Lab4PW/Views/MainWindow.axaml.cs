using System.Linq;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Lab4PW.ViewModels;

namespace Lab4PW.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        
        if (DataContext is MainWindowViewModel vm)
        {
            vm.RequestFileOpen = async () =>
            {
                var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Wybierz obraz BMP",
                    AllowMultiple = false,
                    FileTypeFilter = new[] { new FilePickerFileType("Obrazy") { Patterns = new[] { "*.bmp", "*.jpg", "*.png" } } }
                });
                return files.FirstOrDefault();
            };
        }
    }
}
