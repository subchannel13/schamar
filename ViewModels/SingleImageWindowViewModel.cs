using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Schamar.ViewModels;

public partial class SingleImageWindowViewModel : ViewModelBase
{
    private DirectoryInfo folder;
    private readonly List<FileInfo> files;
    private readonly Random random = new Random();

    public SingleImageWindowViewModel(DirectoryInfo folder)
    {
        this.folder = folder;
        this.files = folder.GetFiles()
            .Where(f => f.Extension.ToLowerInvariant() is ".jpg")
            .ToList();
        this.nextImage();
    }

    [ObservableProperty] private IImage _currentImage;

    private void nextImage()
    {
        var i = random.Next(0, files.Count);
        var file = files[i];
        files.RemoveAt(i);

        using var stream = file.OpenRead();
        this.CurrentImage = new Bitmap(stream);
    }
}