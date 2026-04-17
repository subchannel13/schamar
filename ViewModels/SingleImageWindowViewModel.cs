using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    }

    [ObservableProperty] private IImage _currentImage;
    
    [RelayCommand]
    public void Reject()
    {
        //TODO
        Console.WriteLine("Rejecting");
        
        if (HasNextImage)
            this.NextImage();
    }
    
    public bool HasNextImage => this.files.Any();
    
    public void NextImage()
    {
        var i = random.Next(0, files.Count);
        var file = files[i];
        files.RemoveAt(i);

        using var stream = file.OpenRead();
        this.CurrentImage = new Bitmap(stream);
    }
}