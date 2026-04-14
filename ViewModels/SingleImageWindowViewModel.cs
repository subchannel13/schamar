using System.IO;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Schamar.ViewModels;

public partial class SingleImageWindowViewModel : ViewModelBase
{
    private DirectoryInfo folder;

    public SingleImageWindowViewModel(DirectoryInfo folder)
    {
        this.folder = folder;
    }

    [ObservableProperty] private IImage _currentImage;
}