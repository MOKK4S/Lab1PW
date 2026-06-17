namespace Lab10PW.Models;

public class FastaSequence
{
    public string Id { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Sequence { get; init; } = string.Empty;
    public int Length => Sequence.Length;
}
