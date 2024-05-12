using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TheMovieDb.Shared.Models;

namespace TheMovieDb.Shared.Models
{
    public class Movie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("original_title")]
        public string Original_title { get; set; }
        [JsonPropertyName("overview")]
        public string Overview { get; set; }
        [JsonPropertyName("poster_path")]
        public string Poster_Path { get; set; }
        [JsonPropertyName("release_date")]
        public string Release_Date { get; set; }
        [JsonPropertyName("vote_average")]
        public double Vote_Average { get; set; }
        [JsonPropertyName("genre_ids")]
        public List<int> Genre_Ids { get; set; }

        public Movie()
        {
            
        }

        public Movie(int id, string title, string original_title, string overview, string posterPath, string releaseDate, double voteAverage)
        {
            Id = id;
            Title = title;
            Original_title = original_title;
            Overview = overview;
            Poster_Path = posterPath;
            Release_Date = releaseDate;
            Vote_Average = voteAverage;
        }
    }
}


public record MovieResponse
{
    [JsonPropertyName("results")]
    public required List<Movie> Results { get; set; }
    [JsonPropertyName("page")]
    public int Page { get; set; }
    
}