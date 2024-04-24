using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDb.Shared.Models;

namespace TheMovieDb.Shared.Models
{
    public class MovieVideo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        public MovieVideo()
        {
            
        }

        public MovieVideo(string name, string key)
        {
            Name = name;
            Key = key;
        }

      
    }
}

public record MovieVideoResponse
{
    [JsonProperty("results")]
    public required List<MovieVideo> Results { get; set; }

}