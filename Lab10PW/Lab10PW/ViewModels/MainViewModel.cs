using System.Collections.ObjectModel;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab10PW.Models;
using Lab10PW.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Lab10PW.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<FastaSequence> _sequences = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Analysis))]
    private FastaSequence? _selectedSequence;

    [ObservableProperty]
    private string _statusMessage = "Wczytaj plik FASTA, aby rozpocząć.";

    [ObservableProperty]
    private PlotModel? _chartModel;

    public IStorageProvider? StorageProvider { get; set; }

    public SequenceAnalysis? Analysis => SelectedSequence is not null
        ? SequenceAnalyzer.Analyze(SelectedSequence)
        : null;

    [RelayCommand]
    private async Task LoadFiles()
    {
        if (StorageProvider is null) return;

        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Wybierz plik FASTA",
            AllowMultiple = true,
            FileTypeFilter =
            [
                new FilePickerFileType("FASTA") { Patterns = ["*.fasta", "*.fa", "*.fna"] },
                new FilePickerFileType("Wszystkie pliki") { Patterns = ["*"] }
            ]
        });

        if (files.Count == 0) return;

        Sequences.Clear();
        int loaded = 0;

        foreach (var file in files)
        {
            try
            {
                var parsed = FastaParser.Parse(file.Path.LocalPath);
                foreach (var seq in parsed)
                    Sequences.Add(seq);
                loaded++;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Błąd w pliku {file.Name}: {ex.Message}";
                return;
            }
        }

        StatusMessage = $"Wczytano {Sequences.Count} sekwencji z {loaded} pliku/plików.";
        ChartModel = BuildChartModel();
    }

    [RelayCommand]
    private async Task ExportCsv()
    {
        if (StorageProvider is null || Sequences.Count == 0) return;

        var file = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Eksportuj do CSV",
            SuggestedFileName = "analiza_fasta.csv",
            FileTypeChoices = [new FilePickerFileType("CSV") { Patterns = ["*.csv"] }]
        });

        if (file is null) return;

        try
        {
            ExportService.ExportCsv(Sequences.Select(s => (s, SequenceAnalyzer.Analyze(s))), file.Path.LocalPath);
            StatusMessage = "Eksport CSV zakończony pomyślnie.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Błąd eksportu CSV: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task ExportJson()
    {
        if (StorageProvider is null || Sequences.Count == 0) return;

        var file = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Eksportuj do JSON",
            SuggestedFileName = "analiza_fasta.json",
            FileTypeChoices = [new FilePickerFileType("JSON") { Patterns = ["*.json"] }]
        });

        if (file is null) return;

        try
        {
            ExportService.ExportJson(Sequences.Select(s => (s, SequenceAnalyzer.Analyze(s))), file.Path.LocalPath);
            StatusMessage = "Eksport JSON zakończony pomyślnie.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Błąd eksportu JSON: {ex.Message}";
        }
    }

    private PlotModel BuildChartModel()
    {
        var model = new PlotModel { Title = "Długości sekwencji" };

        model.Axes.Add(new CategoryAxis
        {
            Position = AxisPosition.Bottom,
            ItemsSource = Sequences.Select(s => s.Id).ToList(),
            Angle = -30,
            FontSize = 10
        });
        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = "Długość (bp)",
            Minimum = 0
        });

        var series = new BarSeries
        {
            Title = "Długość",
            FillColor = OxyColor.FromRgb(0, 120, 212),
            StrokeThickness = 0
        };

        foreach (var seq in Sequences)
            series.Items.Add(new BarItem(seq.Length));

        model.Series.Add(series);
        model.IsLegendVisible = false;
        return model;
    }
}
