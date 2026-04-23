using System;
using System.Collections.Generic;
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
    private readonly DispatcherTimer _gameTimer;
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
        _statusMessage = $"Złap {_settings.HyraxCount} Hyraxów!";

        for (int i = 0; i < Columns * Rows; i++) 
            Cells.Add(new GameCellViewModel(i % Columns, i / Columns));

        _gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
        _gameTimer.Tick += (s, e) => CurrentTime += 0.1;

        _spawnTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
        _spawnTimer.Tick += (s, e) => SpawnAnimals();

        _hideTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _hideTimer.Tick += (s, e) => { HideAnimals(); _hideTimer.Stop(); };

        _gameTimer.Start();
        _spawnTimer.Start();
        SpawnAnimals();
    }

    private void SpawnAnimals()
    {
        if (IsGameOver) return;
        HideAnimals();

        var indices = Enumerable.Range(0, Cells.Count).OrderBy(x => _random.Next()).ToList();
        
        // Hyraxy
        for (int i = 0; i < Math.Min(_settings.HyraxCount, indices.Count); i++)
            Cells[indices[i]].CurrentAnimal = AnimalType.Hyrax;

        // Szopy
        int offset = _settings.HyraxCount;
        for (int i = 0; i < Math.Min(_settings.RaccoonCount, indices.Count - offset); i++)
            Cells[indices[offset + i]].CurrentAnimal = AnimalType.Raccoon;

        // Krokodyle
        offset += _settings.RaccoonCount;
        if (_settings.CrocodileCount > 0 && indices.Count > offset)
            Cells[indices[offset]].CurrentAnimal = AnimalType.Crocodile;

        _hideTimer.Start();
    }

    private void HideAnimals()
    {
        foreach (var cell in Cells) cell.CurrentAnimal = AnimalType.None;
    }

    [RelayCommand]
    private void CellClicked(GameCellViewModel cell)
    {
        if (IsGameOver || cell.CurrentAnimal == AnimalType.None) return;

        switch (cell.CurrentAnimal)
        {
            case AnimalType.Hyrax:
                Score++;
                cell.CurrentAnimal = AnimalType.None;
                if (Score >= _settings.HyraxCount) EndGame(true);
                break;
            case AnimalType.Raccoon:
                Score = Math.Max(0, Score - 1);
                cell.CurrentAnimal = AnimalType.None;
                break;
            case AnimalType.Crocodile:
                EndGame(false);
                break;
        }
    }

    private void EndGame(bool success)
    {
        IsGameOver = true;
        _gameTimer.Stop();
        _spawnTimer.Stop();
        _hideTimer.Stop();
        HideAnimals();
        StatusMessage = success ? "SUKCES!" : "PORAŻKA!";
        GameEnded?.Invoke(success, CurrentTime);
    }
}
