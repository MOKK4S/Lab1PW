using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Lab4PW.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private WriteableBitmap? _imageSource;
    [ObservableProperty] private bool _is90Selected = true;
    [ObservableProperty] private bool _is180Selected;
    [ObservableProperty] private bool _is270Selected;

    public Func<Task<IStorageFile?>>? RequestFileOpen { get; set; }

    [RelayCommand]
    private async Task LoadImage()
    {
        if (RequestFileOpen == null) return;
        var file = await RequestFileOpen();
        if (file != null)
        {
            using var stream = await file.OpenReadAsync();
            ImageSource = WriteableBitmap.Decode(stream);
        }
    }

    [RelayCommand]
    private void Rotate()
    {
        if (ImageSource == null) return;
        int angle = Is90Selected ? 90 : Is180Selected ? 180 : 270;
        ImageSource = RotateBitmap(ImageSource, angle);
    }

    private WriteableBitmap RotateBitmap(WriteableBitmap source, int angle)
    {
        int w = (int)source.Size.Width;
        int h = (int)source.Size.Height;
        int newW = (angle == 180) ? w : h;
        int newH = (angle == 180) ? h : w;
        var result = new WriteableBitmap(new PixelSize(newW, newH), source.Dpi, source.Format, source.AlphaFormat);
        using (var srcLock = source.Lock())
        using (var dstLock = result.Lock())
        {
            unsafe
            {
                uint* srcPtr = (uint*)srcLock.Address;
                uint* dstPtr = (uint*)dstLock.Address;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int newX, newY;
                        if (angle == 90) { newX = h - 1 - y; newY = x; }
                        else if (angle == 180) { newX = w - 1 - x; newY = h - 1 - y; }
                        else { newX = y; newY = w - 1 - x; }
                        dstPtr[newY * newW + newX] = srcPtr[y * w + x];
                    }
                }
            }
        }
        return result;
    }

    [RelayCommand]
    private void InvertColors()
    {
        if (ImageSource == null) return;
        using (var l = ImageSource.Lock())
        {
            unsafe
            {
                uint* ptr = (uint*)l.Address;
                int count = (int)(ImageSource.Size.Width * ImageSource.Size.Height);
                for (int i = 0; i < count; i++)
                {
                    uint c = ptr[i];
                    uint a = c & 0xFF000000;
                    uint r = 255 - ((c >> 16) & 0xFF);
                    uint g = 255 - ((c >> 8) & 0xFF);
                    uint b = 255 - (c & 0xFF);
                    ptr[i] = a | (r << 16) | (g << 8) | b;
                }
            }
        }
        OnPropertyChanged(nameof(ImageSource));
    }

    [RelayCommand]
    private void UpsideDown()
    {
        if (ImageSource == null) return;
        int w = (int)ImageSource.Size.Width;
        int h = (int)ImageSource.Size.Height;
        using (var l = ImageSource.Lock())
        {
            unsafe
            {
                uint* ptr = (uint*)l.Address;
                for (int y = 0; y < h / 2; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        uint temp = ptr[y * w + x];
                        ptr[y * w + x] = ptr[(h - 1 - y) * w + x];
                        ptr[(h - 1 - y) * w + x] = temp;
                    }
                }
            }
        }
        OnPropertyChanged(nameof(ImageSource));
    }

    [RelayCommand]
    private void OnlyGreen()
    {
        if (ImageSource == null) return;
        using (var l = ImageSource.Lock())
        {
            unsafe
            {
                uint* ptr = (uint*)l.Address;
                int count = (int)(ImageSource.Size.Width * ImageSource.Size.Height);
                for (int i = 0; i < count; i++)
                {
                    uint c = ptr[i];
                    uint r = (c >> 16) & 0xFF;
                    uint g = (c >> 8) & 0xFF;
                    uint b = c & 0xFF;
                    if (!(g > r * 1.2 && g > b * 1.2 && g > 50))
                    {
                        ptr[i] = 0xFF000000;
                    }
                }
            }
        }
        OnPropertyChanged(nameof(ImageSource));
    }
}
