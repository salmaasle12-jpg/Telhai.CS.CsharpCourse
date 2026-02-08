using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Telhai.CS.CsharpCourse.PlayerProject
{
    public class SongMetadataCacheService
    {
        private const string FILE_NAME = "songs_metadata.json";
        private List<SongMetadata> _cache;

        public SongMetadataCacheService()
        {
            Load();
        }

        private void Load()
        {
            if (File.Exists(FILE_NAME))
            {
                var json = File.ReadAllText(FILE_NAME);
                _cache = JsonSerializer.Deserialize<List<SongMetadata>>(json)
                         ?? new List<SongMetadata>();
            }
            else
            {
                _cache = new List<SongMetadata>();
            }
        }

        private void Save()
        {
            var json = JsonSerializer.Serialize(_cache,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FILE_NAME, json);
        }

        public SongMetadata GetByPath(string path)
        {
            return _cache.FirstOrDefault(x => x.FilePath == path);
        }

        public void SaveOrUpdate(SongMetadata meta)
        {
            var existing = GetByPath(meta.FilePath);
            if (existing != null)
                _cache.Remove(existing);

            _cache.Add(meta);
            Save();
        }
    }
}

