using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab7PW.Models;

namespace Lab7PW.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string _sequenceInput = string.Empty;
    [ObservableProperty] private string _statusMessage = string.Empty;

    public ObservableCollection<KmerResult> Results { get; } = new();

    [RelayCommand]
    private void Clear()
    {
        SequenceInput = string.Empty;
        StatusMessage = string.Empty;
        Results.Clear();
    }

    [RelayCommand]
    private void Analyze()
    {
        Results.Clear();

        if (string.IsNullOrWhiteSpace(SequenceInput))
        {
            StatusMessage = "Wprowadź sekwencję DNA.";
            return;
        }

        var counts = DnaAnalyzer.CountKmers(SequenceInput);

        if (counts.Count == 0)
        {
            StatusMessage = "Nie znaleziono sekwencji 4-nukleotydowych.";
            return;
        }

        foreach (var entry in counts.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            Results.Add(new KmerResult(entry.Key, entry.Value));

        StatusMessage = $"Znaleziono {counts.Count} unikalnych sekwencji.";
    }
}

public record KmerResult(string Kmer, int Count);
