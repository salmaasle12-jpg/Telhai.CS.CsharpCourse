using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telhai.CS.CsharpCourse.PlayerProject.Models
{
    public class ItunesSearchResponse
    {
        [JsonPropertyName("resultCount")]
        public int ResultCount { get; set; }

        [JsonPropertyName("results")]
        public List<ItunesResultItem>? Results { get; set; }
    }
}
