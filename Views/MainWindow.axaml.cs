using System;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
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
        var folder = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Open Text File",
            AllowMultiple = false,
        });

        if (folder.Any())
            (this.DataContext as MainWindowViewModel)!.InputPath = folder[0].Path.LocalPath;
    }
    
    private async void BrowseOutput_Click(object sender, RoutedEventArgs e) 
    {
        var folder = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Open Text File",
            AllowMultiple = false,
        });

        if (folder.Any())
            (this.DataContext as MainWindowViewModel)!.OutputPath = folder[0].Path.LocalPath;
    }

    private async void Start_OnClick(object? sender, RoutedEventArgs e)
    {
        var folder = new DirectoryInfo((this.DataContext as MainWindowViewModel)!.InputPath);
        if (!folder.Exists)
            return;

        var viewModel = new SingleImageWindowViewModel(folder);
        if (!viewModel.HasNextImage) return;
        
        viewModel.NextImage();
        var window = new SingleImageWindow
        {
            DataContext = viewModel
        };
        await window.ShowDialog(this);
    }
}