using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Schamar.Models;
using Schamar.ViewModels;

namespace Schamar.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private async void BrowseInput_Click(object sender, RoutedEventArgs e) 
    {
        var viewModel = (MainWindowViewModel)DataContext!;
        var folder = await PickFolderAsync("Select Input Folder", viewModel.InputPath);

        if (folder is not null)
            viewModel.InputPath = folder;
    }
    
    private async void BrowseOutput_Click(object sender, RoutedEventArgs e) 
    {
        var viewModel = (MainWindowViewModel)DataContext!;
        var folder = await PickFolderAsync("Select Output Folder", viewModel.OutputPath);

        if (folder is not null)
            viewModel.OutputPath = folder;
    }

    private async Task<string?> PickFolderAsync(string title, string? currentPath)
    {
        var startLocation = await GetExistingFolderAsync(currentPath);

        var folders = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
            SuggestedStartLocation = startLocation,
        });

        return folders.Count > 0 ? folders[0].Path.LocalPath : null;
    }

    private async Task<IStorageFolder?> GetExistingFolderAsync(string? path)
    {
        if (string.IsNullOrWhiteSpace(path) || !Path.IsPathRooted(path) || !Directory.Exists(path))
            return null;

        return await StorageProvider.TryGetFolderFromPathAsync(path);
    }

    private async void Start_OnClick(object? sender, RoutedEventArgs e)
    {
        var folder = new DirectoryInfo((this.DataContext as MainWindowViewModel)!.InputPath);
        if (!folder.Exists)
            return;

        var sorter = new FileSorter(folder);
        var viewModel = new SingleImageWindowViewModel(sorter);
        if (!viewModel.HasNextImage) return;
        
        viewModel.UpdateImage();
        var sortWindow = new SingleImageWindow
        {
            DataContext = viewModel
        };
        sorter.OnFinish += sortWindow.Close;
        await sortWindow.ShowDialog(this);

        var resultViewModel = new ResultsWindowViewModel(sorter);
        var resultWindow = new ResultsWindow
        {
            DataContext = resultViewModel
        };

        if (!await resultWindow.ShowDialog<bool>(this)) 
            return;
        
        var destination = new DirectoryInfo((this.DataContext as MainWindowViewModel)!.OutputPath);
        destination.Create();
        foreach (var file in sorter.Decisions())
        {
            switch (file.Decision)
            {
                case SortDecision.Accepted:
                    file.File.CopyTo(Path.Combine(destination.FullName, file.File.Name));
                    file.File.Delete();
                    break;
                case SortDecision.Rejected:
                    file.File.Delete();
                    break;
            }
        }
    }
}