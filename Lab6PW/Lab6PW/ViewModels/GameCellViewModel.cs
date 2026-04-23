using CommunityToolkit.Mvvm.ComponentModel;
using Lab6PW.Models;
namespace Lab6PW.ViewModels;
public partial class GameCellViewModel : ViewModelBase {
    [ObservableProperty] private AnimalType _currentAnimal = AnimalType.None;
    public int X { get; } public int Y { get; }
    public GameCellViewModel(int x, int y) { X = x; Y = y; }
}
