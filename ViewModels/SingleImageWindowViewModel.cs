using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Schamar.Models;

namespace Schamar.ViewModels;

public partial class SingleImageWindowViewModel : ViewModelBase
{
    private readonly FileSorter sorter;

    public SingleImageWindowViewModel(FileSorter sorter)
    {
        this.sorter = sorter;
    }

    [ObservableProperty] private IImage _currentImage;
    
    [RelayCommand]
    public void Reject()
    {
        this.sorter.Reject();
        if (HasNextImage)
            this.UpdateImage();
    }
    
    [RelayCommand]
    public void Accept()
    {
        this.sorter.Accept();
        if (HasNextImage)
            this.UpdateImage();
    }

    [RelayCommand]
    public void Finish()
    {
        this.sorter.Finish();
    }

    public bool HasNextImage => this.sorter.Current != null;
    
    public void UpdateImage()
    {
        var file = this.sorter.Current!;
        
        using var stream = file.OpenRead();
        this.CurrentImage = new Bitmap(stream);
    }
}