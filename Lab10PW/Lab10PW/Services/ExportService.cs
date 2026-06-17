using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Lab10PW.Models;
using Newtonsoft.Json;

namespace Lab10PW.Services;

public static class ExportService
{
    public static void ExportCsv(IEnumerable<(FastaSequence Sequence, SequenceAnalysis Analysis)> data, string filePath)
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));

        csv.WriteHeader<ExportRecord>();
        csv.NextRecord();

        foreach (var (seq, analysis) in data)
        {
            csv.WriteRecord(ToRecord(seq, analysis));
            csv.NextRecord();
        }
    }

    public static void ExportJson(IEnumerable<(FastaSequence Sequence, SequenceAnalysis Analysis)> data, string filePath)
    {
        var records = data.Select(d => ToRecord(d.Sequence, d.Analysis));
        File.WriteAllText(filePath, JsonConvert.SerializeObject(records, Formatting.Indented));
    }

    private static ExportRecord ToRecord(FastaSequence seq, SequenceAnalysis analysis) => new()
    {
        Id = seq.Id,
        Description = seq.Description,
        Length = analysis.Length,
        GcContent = analysis.GcContent,
        CodonCount = analysis.CodonCount,
        CountA = analysis.CountA,
        CountT = analysis.CountT,
        CountG = analysis.CountG,
        CountC = analysis.CountC,
        CountOther = analysis.CountOther
    };

    private class ExportRecord
    {
        public string Id { get; set; } = "";
        public string Description { get; set; } = "";
        public int Length { get; set; }
        public double GcContent { get; set; }
        public int CodonCount { get; set; }
        public int CountA { get; set; }
        public int CountT { get; set; }
        public int CountG { get; set; }
        public int CountC { get; set; }
        public int CountOther { get; set; }
    }
}
