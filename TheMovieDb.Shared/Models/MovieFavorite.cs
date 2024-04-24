using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDb.Shared.Models
{

    public class MovieFavorite
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("original_title")]
        public string Original_title { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("poster_path")]
        public string Poster_Path { get; set; }
        [JsonProperty("release_date")]
        public string Release_Date { get; set; }
        [JsonProperty("vote_average")]
        public double Vote_Average { get; set; }
      

        public MovieFavorite()
        {

        }

        public MovieFavorite(int id, string title, string original_title, string overview, string posterPath, string releaseDate, double voteAverage)
        {
            id = id;
            Title = title;
            Original_title = original_title;
            Overview = overview;
            Poster_Path = posterPath;
            Release_Date = releaseDate;
            Vote_Average = voteAverage;
        }
    }


}
