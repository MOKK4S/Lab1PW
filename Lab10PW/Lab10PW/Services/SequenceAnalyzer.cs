using Lab10PW.Models;

namespace Lab10PW.Services;

public static class SequenceAnalyzer
{
    public static SequenceAnalysis Analyze(FastaSequence sequence)
    {
        var seq = sequence.Sequence.ToUpper();
        int a = 0, t = 0, g = 0, c = 0, other = 0;

        foreach (var ch in seq)
        {
            switch (ch)
            {
                case 'A': a++; break;
                case 'T': t++; break;
                case 'G': g++; break;
                case 'C': c++; break;
                default: other++; break;
            }
        }

        double gcContent = seq.Length > 0
            ? Math.Round((double)(g + c) / seq.Length * 100.0, 2)
            : 0.0;

        return new SequenceAnalysis
        {
            Length = seq.Length,
            GcContent = gcContent,
            CodonCount = seq.Length / 3,
            CountA = a,
            CountT = t,
            CountG = g,
            CountC = c,
            CountOther = other
        };
    }
}
