using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab6PW.Models;

namespace Lab6PW.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // NumericUpDown w Avalonii wymaga decimal, żeby bindowanie było stabilne
    [ObservableProperty] private decimal _boardX = 3;
    [ObservableProperty] private decimal _boardY = 3;
    [ObservableProperty] private decimal _hyraxCount = 1;
    [ObservableProperty] private decimal _raccoonCount = 2;
    [ObservableProperty] private decimal _crocodileCount = 0;

    [ObservableProperty] private bool _isSettingsVisible = false;

    public ObservableCollection<ScoreEntry> Scores { get; } = new();

    [RelayCommand]
    private void ToggleSettings() => IsSettingsVisible = !IsSettingsVisible;

    [RelayCommand]
    private void Exit() => System.Environment.Exit(0);
}
