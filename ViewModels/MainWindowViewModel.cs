using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Schamar.Services;

namespace Schamar.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly SettingsService settingsService;
        
    public MainWindowViewModel(SettingsService settingsService)
    {
        this.settingsService = settingsService;
        InputPath = settingsService.Settings.InputFolder;
        OutputPath = settingsService.Settings.OutputFolder;
    }
    
    [ObservableProperty] private string _inputPath = "";
    [ObservableProperty] private string _outputPath = "";

    partial void OnInputPathChanged(string value)
    {
        settingsService.Settings.InputFolder = value;
        settingsService.Save();
    }

    partial void OnOutputPathChanged(string value)
    {
        settingsService.Settings.OutputFolder = value;
        settingsService.Save();
    }
}