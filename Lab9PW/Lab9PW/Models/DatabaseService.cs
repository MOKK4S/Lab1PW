using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Lab9PW.Models;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string dbPath)
    {
        _connectionString = $"Data Source={dbPath};Version=3;";
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Wnioski (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Miejscowosc TEXT,
                DataZlozenia TEXT,
                ImieNazwisko TEXT,
                NumerAlbumu TEXT,
                KierunekStudiow TEXT,
                Specjalnosc TEXT,
                RokStudiow TEXT,
                Semestr TEXT,
                TrybStudiow TEXT,
                NazwaPrzedmiotu TEXT,
                ImieNazwiskoEgzaminatora TEXT,
                TerminPierwszy TEXT,
                TerminDrugi TEXT,
                UzasadnieniWniosku TEXT,
                ProponowanyTermin TEXT
            )";
        command.ExecuteNonQuery();
    }

    public void SaveWniosek(WniosekModel w)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Wnioski (
                Miejscowosc, DataZlozenia, ImieNazwisko, NumerAlbumu,
                KierunekStudiow, Specjalnosc, RokStudiow, Semestr,
                TrybStudiow, NazwaPrzedmiotu, ImieNazwiskoEgzaminatora,
                TerminPierwszy, TerminDrugi, UzasadnieniWniosku, ProponowanyTermin
            ) VALUES (
                @Miejscowosc, @DataZlozenia, @ImieNazwisko, @NumerAlbumu,
                @KierunekStudiow, @Specjalnosc, @RokStudiow, @Semestr,
                @TrybStudiow, @NazwaPrzedmiotu, @ImieNazwiskoEgzaminatora,
                @TerminPierwszy, @TerminDrugi, @UzasadnieniWniosku, @ProponowanyTermin
            )";
        command.Parameters.AddWithValue("@Miejscowosc", w.Miejscowosc);
        command.Parameters.AddWithValue("@DataZlozenia", w.DataZlozenia);
        command.Parameters.AddWithValue("@ImieNazwisko", w.ImieNazwisko);
        command.Parameters.AddWithValue("@NumerAlbumu", w.NumerAlbumu);
        command.Parameters.AddWithValue("@KierunekStudiow", w.KierunekStudiow);
        command.Parameters.AddWithValue("@Specjalnosc", w.Specjalnosc);
        command.Parameters.AddWithValue("@RokStudiow", w.RokStudiow);
        command.Parameters.AddWithValue("@Semestr", w.Semestr);
        command.Parameters.AddWithValue("@TrybStudiow", w.TrybStudiow);
        command.Parameters.AddWithValue("@NazwaPrzedmiotu", w.NazwaPrzedmiotu);
        command.Parameters.AddWithValue("@ImieNazwiskoEgzaminatora", w.ImieNazwiskoEgzaminatora);
        command.Parameters.AddWithValue("@TerminPierwszy", w.TerminPierwszy);
        command.Parameters.AddWithValue("@TerminDrugi", w.TerminDrugi);
        command.Parameters.AddWithValue("@UzasadnieniWniosku", w.UzasadnieniWniosku);
        command.Parameters.AddWithValue("@ProponowanyTermin", w.ProponowanyTermin);
        command.ExecuteNonQuery();
    }

    public List<WniosekModel> LoadAllWnioski()
    {
        var lista = new List<WniosekModel>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Wnioski ORDER BY Id DESC";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new WniosekModel
            {
                Id = reader.GetInt32(0),
                Miejscowosc = reader.GetString(1),
                DataZlozenia = reader.GetString(2),
                ImieNazwisko = reader.GetString(3),
                NumerAlbumu = reader.GetString(4),
                KierunekStudiow = reader.GetString(5),
                Specjalnosc = reader.GetString(6),
                RokStudiow = reader.GetString(7),
                Semestr = reader.GetString(8),
                TrybStudiow = reader.GetString(9),
                NazwaPrzedmiotu = reader.GetString(10),
                ImieNazwiskoEgzaminatora = reader.GetString(11),
                TerminPierwszy = reader.GetString(12),
                TerminDrugi = reader.GetString(13),
                UzasadnieniWniosku = reader.GetString(14),
                ProponowanyTermin = reader.GetString(15)
            });
        }
        return lista;
    }
}
