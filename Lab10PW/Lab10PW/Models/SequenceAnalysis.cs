namespace Lab10PW.Models;

public class SequenceAnalysis
{
    public int Length { get; init; }
    public double GcContent { get; init; }
    public int CodonCount { get; init; }
    public int CountA { get; init; }
    public int CountT { get; init; }
    public int CountG { get; init; }
    public int CountC { get; init; }
    public int CountOther { get; init; }
}
