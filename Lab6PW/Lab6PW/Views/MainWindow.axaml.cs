using System;
using Avalonia.Controls;
using Lab6PW.ViewModels;
using Lab6PW.Models;
namespace Lab6PW.Views;
public partial class MainWindow : Window {
    public MainWindow() { InitializeComponent(); }
    public void StartGame_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
        if (DataContext is MainWindowViewModel vm) {
            var gvm = new GameViewModel(new GameSettings { BoardWidth = vm.BoardX, BoardHeight = vm.BoardY, HyraxCount = vm.HyraxCount });
            var gw = new GameWindow { DataContext = gvm };
            gvm.GameEnded += (s, t) => { if (s) vm.Scores.Add(new ScoreEntry { TimeInSeconds = t, Date = DateTime.Now }); };
            gw.ShowDialog(this);
        }
    }
}
