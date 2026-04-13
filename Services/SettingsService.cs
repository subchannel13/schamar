using System;
using System.IO;
using System.Text.Json;
using Schamar.Models;

namespace Schamar.Services;

public class SettingsService
{
    private readonly string filePath;
    private readonly JsonSerializerOptions serializerOptions = new()
    {
        WriteIndented = true
    };

    public Settings Settings { get; }
    
    public SettingsService()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var folder = Path.Combine(appData, "Schamar");

        Directory.CreateDirectory(folder);
        filePath = Path.Combine(folder, "userSettings.json");

        Settings = Load();
    }

    private Settings Load()
    {
        if (!File.Exists(filePath))
            return new Settings();

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(Settings, serializerOptions);

        File.WriteAllText(filePath, json);
    }
}