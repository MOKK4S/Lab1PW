using System;
using Avalonia.Controls;
using Lab6PW.ViewModels;
using Lab6PW.Models;

namespace Lab6PW.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void StartGame_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            var settings = new GameSettings
            {
                BoardWidth = vm.BoardX,
                BoardHeight = vm.BoardY,
                HyraxCount = vm.HyraxCount,
                RaccoonCount = vm.RaccoonCount,
                CrocodileCount = vm.CrocodileCount
            };

            var gameVM = new GameViewModel(settings);
            var gameWindow = new GameWindow { DataContext = gameVM };

            gameVM.GameEnded += (success, time) =>
            {
                if (success)
                {
                    vm.Scores.Add(new ScoreEntry { TimeInSeconds = time, Date = DateTime.Now });
                }
            };

            gameWindow.ShowDialog(this);
        }
    }
}
