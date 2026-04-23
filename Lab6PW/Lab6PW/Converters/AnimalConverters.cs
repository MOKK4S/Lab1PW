using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Lab6PW.Models;
namespace Lab6PW.Converters;
public class AnimalToIconConverter : IValueConverter {
    public object? Convert(object? v, Type t, object? p, CultureInfo c) => v is AnimalType a ? a switch { AnimalType.Hyrax => "🐹", AnimalType.Raccoon => "🦝", AnimalType.Crocodile => "🐊", _ => "🗑️" } : "🗑️";
    public object? ConvertBack(object? v, Type t, object? p, CultureInfo c) => throw new NotImplementedException();
}
