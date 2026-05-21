using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab9PW.Models;

namespace Lab9PW.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DatabaseService _db;

    [ObservableProperty] private string _miejscowosc = string.Empty;
    [ObservableProperty] private string _dataZlozenia = string.Empty;
    [ObservableProperty] private string _imieNazwisko = string.Empty;
    [ObservableProperty] private string _numerAlbumu = string.Empty;
    [ObservableProperty] private string _kierunekStudiow = string.Empty;
    [ObservableProperty] private string _specjalnosc = string.Empty;
    [ObservableProperty] private string _rokStudiow = string.Empty;
    [ObservableProperty] private string _semestr = string.Empty;
    [ObservableProperty] private string _trybStudiow = string.Empty;
    [ObservableProperty] private string _nazwaPrzedmiotu = string.Empty;
    [ObservableProperty] private string _imieNazwiskoEgzaminatora = string.Empty;
    [ObservableProperty] private string _terminPierwszy = string.Empty;
    [ObservableProperty] private string _terminDrugi = string.Empty;
    [ObservableProperty] private string _uzasadnieniWniosku = string.Empty;
    [ObservableProperty] private string _proponowanyTermin = string.Empty;

    [ObservableProperty] private string _statusMessage = string.Empty;
    [ObservableProperty] private WniosekModel? _wybranyWniosek;

    public ObservableCollection<WniosekModel> Wnioski { get; } = new();

    public MainWindowViewModel()
    {
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wnioski.db");
        _db = new DatabaseService(dbPath);
        WczytajWnioski();
    }

    [RelayCommand]
    private void ZapiszWniosek()
    {
        var wniosek = new WniosekModel
        {
            Miejscowosc = Miejscowosc,
            DataZlozenia = DataZlozenia,
            ImieNazwisko = ImieNazwisko,
            NumerAlbumu = NumerAlbumu,
            KierunekStudiow = KierunekStudiow,
            Specjalnosc = Specjalnosc,
            RokStudiow = RokStudiow,
            Semestr = Semestr,
            TrybStudiow = TrybStudiow,
            NazwaPrzedmiotu = NazwaPrzedmiotu,
            ImieNazwiskoEgzaminatora = ImieNazwiskoEgzaminatora,
            TerminPierwszy = TerminPierwszy,
            TerminDrugi = TerminDrugi,
            UzasadnieniWniosku = UzasadnieniWniosku,
            ProponowanyTermin = ProponowanyTermin
        };
        _db.SaveWniosek(wniosek);
        WczytajWnioski();
        StatusMessage = "Wniosek zapisany pomyślnie.";
        WyczyscFormularz();
    }

    [RelayCommand]
    private void WczytajWnioski()
    {
        Wnioski.Clear();
        foreach (var w in _db.LoadAllWnioski())
            Wnioski.Add(w);
    }

    [RelayCommand]
    private void WczytajWybrany()
    {
        if (WybranyWniosek is null) return;
        var w = WybranyWniosek;
        Miejscowosc = w.Miejscowosc;
        DataZlozenia = w.DataZlozenia;
        ImieNazwisko = w.ImieNazwisko;
        NumerAlbumu = w.NumerAlbumu;
        KierunekStudiow = w.KierunekStudiow;
        Specjalnosc = w.Specjalnosc;
        RokStudiow = w.RokStudiow;
        Semestr = w.Semestr;
        TrybStudiow = w.TrybStudiow;
        NazwaPrzedmiotu = w.NazwaPrzedmiotu;
        ImieNazwiskoEgzaminatora = w.ImieNazwiskoEgzaminatora;
        TerminPierwszy = w.TerminPierwszy;
        TerminDrugi = w.TerminDrugi;
        UzasadnieniWniosku = w.UzasadnieniWniosku;
        ProponowanyTermin = w.ProponowanyTermin;
        StatusMessage = $"Wczytano wniosek #{w.Id}.";
    }

    [RelayCommand]
    private void WyczyscFormularz()
    {
        Miejscowosc = string.Empty;
        DataZlozenia = string.Empty;
        ImieNazwisko = string.Empty;
        NumerAlbumu = string.Empty;
        KierunekStudiow = string.Empty;
        Specjalnosc = string.Empty;
        RokStudiow = string.Empty;
        Semestr = string.Empty;
        TrybStudiow = string.Empty;
        NazwaPrzedmiotu = string.Empty;
        ImieNazwiskoEgzaminatora = string.Empty;
        TerminPierwszy = string.Empty;
        TerminDrugi = string.Empty;
        UzasadnieniWniosku = string.Empty;
        ProponowanyTermin = string.Empty;
    }
}
