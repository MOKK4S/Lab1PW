namespace AvaloniaApplication2.Models;

public class Product
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"{Name} - {Price:C}";
    }
}
