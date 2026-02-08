using System.IO;
using System.Text.Json;

namespace Telhai.CS.CsharpCourse.PlayerProject
{
    public class AppSettings
    {
        public List<string> MusicFolders { get; set; } = new List<string>();
        private const string SETTINGS_FILE = "settings.json";

        public static void Save(AppSettings settings)
        {
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SETTINGS_FILE, json);
        }

        public static AppSettings Load()
        {
            if (File.Exists(SETTINGS_FILE))
            {
                string json = File.ReadAllText(SETTINGS_FILE);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
            return new AppSettings();
        }
    }
}
