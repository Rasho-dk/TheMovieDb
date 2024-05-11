using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TheMovieDb.Shared.Models;

namespace TheMovieDb.Shared.Models
{
    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

public record GenreResponse
{
    [JsonPropertyName("genres")]
    public required List<Genre> Genres { get; set; }
}