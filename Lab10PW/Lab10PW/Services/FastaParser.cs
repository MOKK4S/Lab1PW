using Lab10PW.Models;

namespace Lab10PW.Services;

public static class FastaParser
{
    public static List<FastaSequence> Parse(string filePath)
    {
        var sequences = new List<FastaSequence>();
        string? currentId = null;
        string? currentDescription = null;
        var currentSeq = new System.Text.StringBuilder();

        foreach (var line in File.ReadLines(filePath))
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith('>'))
            {
                if (currentId != null)
                    sequences.Add(Build(currentId, currentDescription ?? "", currentSeq.ToString()));

                var header = trimmed[1..];
                var spaceIdx = header.IndexOf(' ');
                if (spaceIdx >= 0)
                {
                    currentId = header[..spaceIdx];
                    currentDescription = header[(spaceIdx + 1)..];
                }
                else
                {
                    currentId = header;
                    currentDescription = string.Empty;
                }
                currentSeq.Clear();
            }
            else if (!string.IsNullOrWhiteSpace(trimmed))
            {
                if (!trimmed.All(char.IsLetter))
                    throw new FormatException($"Nieprawidłowa linia sekwencji FASTA: {trimmed}");
                currentSeq.Append(trimmed.ToUpper());
            }
        }

        if (currentId != null)
            sequences.Add(Build(currentId, currentDescription ?? "", currentSeq.ToString()));

        if (sequences.Count == 0)
            throw new FormatException("Plik nie zawiera żadnych sekwencji FASTA.");

        return sequences;
    }

    private static FastaSequence Build(string id, string desc, string seq) =>
        new() { Id = id, Description = desc, Sequence = seq };
}
