using Avalonia;
using System;
using Microsoft.Extensions.DependencyInjection;
using Schamar.Services;
using Schamar.ViewModels;

namespace Schamar;

sealed class Program
{
    public static IServiceProvider Services { get; private set; }
    
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddSingleton<SettingsService>();
        services.AddTransient<MainWindowViewModel>();

        Services = services.BuildServiceProvider();
        
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}