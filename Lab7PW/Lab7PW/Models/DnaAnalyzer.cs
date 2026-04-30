using System.Collections.Generic;
using System.Linq;

namespace Lab7PW.Models;

public static class DnaAnalyzer
{
    private const int KmerLength = 4;
    private static readonly HashSet<char> ValidBases = ['A', 'C', 'G', 'T'];

    public static Dictionary<string, int> CountKmers(string sequence)
    {
        var counts = new Dictionary<string, int>();
        var upper = sequence.ToUpperInvariant();

        for (int i = 0; i <= upper.Length - KmerLength; i++)
        {
            var kmer = upper.Substring(i, KmerLength);
            if (kmer.All(c => ValidBases.Contains(c)))
            {
                counts.TryGetValue(kmer, out var current);
                counts[kmer] = current + 1;
            }
        }

        return counts;
    }
}
