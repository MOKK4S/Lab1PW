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
                BoardWidth = (int)vm.BoardX,
                BoardHeight = (int)vm.BoardY,
                HyraxCount = (int)vm.HyraxCount,
                RaccoonCount = (int)vm.RaccoonCount,
                CrocodileCount = (int)vm.CrocodileCount
            };

            var gameVM = new GameViewModel(settings);
            var gameWindow = new GameWindow { DataContext = gameVM };

            gameVM.GameEnded += (success, time) =>
            {
                if (success)
                {
                    vm.Scores.Add(new ScoreEntry { TimeInSeconds = Math.Round(time, 2), Date = DateTime.Now });
                }
            };

            gameWindow.ShowDialog(this);
        }
    }
}
