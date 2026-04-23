using Avalonia.Controls;
using Avalonia.Interactivity;
namespace Lab6PW.Views;
public partial class GameWindow : Window {
    public GameWindow() { InitializeComponent(); }
    private void CloseButton_Click(object? sender, RoutedEventArgs e) => Close();
}
