using System.Collections.Generic;

namespace Telhai.CS.CsharpCourse.PlayerProject
{
    public class SongMetadata
    {
        public string FilePath { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string CoverUrl { get; set; }
        public List<string> ExtraImagePaths { get; set; } = new();
    }
}
