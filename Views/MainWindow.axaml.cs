using System;
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

        (this.DataContext as MainWindowViewModel)!.InputPath = folder[0].Path.LocalPath;
    }
}