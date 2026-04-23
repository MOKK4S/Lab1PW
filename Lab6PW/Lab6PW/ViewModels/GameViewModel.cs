using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab6PW.Models;

namespace Lab6PW.ViewModels;

public partial class GameViewModel : ViewModelBase
{
    private readonly GameSettings _settings;
    private readonly DispatcherTimer _spawnTimer;
    private readonly DispatcherTimer _hideTimer;
    private readonly Random _random = new();

    [ObservableProperty] private int _score = 0;
    [ObservableProperty] private double _currentTime = 0;
    [ObservableProperty] private string _statusMessage = "";
    [ObservableProperty] private bool _isGameOver = false;

    public ObservableCollection<GameCellViewModel> Cells { get; } = new();
    public int Columns => _settings.BoardWidth;
    public int Rows => _settings.BoardHeight;
    public event Action<bool, double>? GameEnded;

    public GameViewModel(GameSettings settings)
    {
        _settings = settings;
        for (int i = 0; i < Columns * Rows; i++) Cells.Add(new GameCellViewModel(i % Columns, i / Columns));
        _spawnTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
        _spawnTimer.Tick += (s, e) => Spawn();
        _hideTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _hideTimer.Tick += (s, e) => { Hide(); _hideTimer.Stop(); };
        _spawnTimer.Start();
        Spawn();
    }

    private void Spawn() {
        Hide();
        var idx = _random.Next(Cells.Count);
        Cells[idx].CurrentAnimal = AnimalType.Hyrax;
        _hideTimer.Start();
    }

    private void Hide() { foreach (var c in Cells) c.CurrentAnimal = AnimalType.None; }

    [RelayCommand]
    private void CellClicked(GameCellViewModel cell) {
        if (cell.CurrentAnimal == AnimalType.Hyrax) {
            Score++;
            if (Score >= _settings.HyraxCount) End(true);
        }
    }

    private void End(bool win) {
        IsGameOver = true;
        _spawnTimer.Stop();
        GameEnded?.Invoke(win, CurrentTime);
    }
}
