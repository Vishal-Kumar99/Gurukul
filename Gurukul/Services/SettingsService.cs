
using Gurukul.MVVM.Models;
using System.IO;
using System.Text.Json;

namespace Gurukul.Services;

public static class SettingsService
{
    public static readonly string SettingsFilePath = "appsettings.json";

    public static AppSettings LoadSettings()
    {
        if (!File.Exists(SettingsFilePath))
        {
            return new AppSettings();
        }
        var json = File.ReadAllText(SettingsFilePath);
        return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
    }

    public static void SaveSettings(AppSettings settings)
    {
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsFilePath, json);
    }
}
