using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Helpers;
using TheMovieDb.Shared.Models;
using NetJSON;
using static TheMovieDb.Shared.Models.MovieVideo;
using System.Text;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace TheMovieDbWebApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> GetAll(int? newPage = null)
        {
            var response = await _httpClient.GetAsync(newPage.HasValue ? $"discover/movie?page={newPage}" : "discover/movie");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                //var result = JsonConvert.DeserializeObject<MovieResponse>(json);

                var result = JsonConvert.DeserializeObject<MovieResponse>(json);


                //Did not work
                //var result = NetJSON.NetJSON.Deserialize<MovieResponse>(json);

                return result.Results;
            }
            else
            {
                return null;
            }
            #region Old Code
            //using(var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://api.themoviedb.org/3/discover/movie"); // Base URL for API Controller
            //    client.DefaultRequestHeaders.Accept.Clear();// Clear the default headers
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set the Accept header for JSON
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" + "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmODRlOTkzZWNkODY1ZGE4N2Y2OTlmODIyODdkMGEzNSIsInN1YiI6IjY1ZDBjNmNhMzAzYzg1MDE2NTUyYWFmZiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.lyI2AAUWSCJ_IdlN8rf3eBEZp80TCdQYaYSyGGnP_Es"); // Set the Authorization header
            //    var response = await client.GetAsync("http://api.themoviedb.org/3/discover/movie"); // Send the GET request
            //    if (response.IsSuccessStatusCode) // Check if the response is successful
            //    {
            //        var json = await response.Content.ReadAsStringAsync(); // Read the response as a string
            //        var result = JsonConvert.DeserializeObject<MovieResponse>(json); // Deserialize the JSON string to an object
            //        return result.Results; // Return the results
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            #endregion


            #region Old Code
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri("http://api.themoviedb.org/3/discover/movie"),
            //    Headers =
            //    {
            //        { "accept", "application/json" },

            //       { "Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmODRlOTkzZWNkODY1ZGE4N2Y2OTlmODIyODdkMGEzNSIsInN1YiI6IjY1ZDBjNmNhMzAzYzg1MDE2NTUyYWFmZiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.lyI2AAUWSCJ_IdlN8rf3eBEZp80TCdQYaYSyGGnP_Es" },

            //    }

            //};

            //var response = await _httpClient.SendAsync(request);

            //if (response.IsSuccessStatusCode)
            //{
            //    //await response.Content.ReadAsStringAsync();

            //    var json = await response.Content.ReadAsStringAsync();

            //    var result = JsonConvert.DeserializeObject<MovieResponse>(json);

            //    return result.Results;


            //}
            //else
            //{
            //    return null;
            //}
            #endregion
        }

        public async Task<List<Movie>> SearchMovie(string query)
        {
            var response = await _httpClient.GetAsync($"search/movie?query={query}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieResponse>(json);
                return result.Results;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<MovieVideo>> GetMovieVideo(int id)
        {
            //_httpClient.BaseAddress = new Uri("http://api.themoviedb.org/3/"); 
            var response = await _httpClient.GetAsync($"movie/{id}/videos");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieVideoResponse>(json);
                return result.Results;
            }
            else
            {
                return null;
            }
        }
        public async Task<Movie> AddMovie(Movie movie)
        {
            Dictionary<string, object> headers = new Dictionary<string, object>();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(movie); // Serialize the movie object to a JSON string
            var jsonObj = JObject.Parse(json).Property("Id").Name.ToLower();// Get the JSON object from the JSON string and convert the property name to lowercase.  
            json = json.Replace("Id", jsonObj);               
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Create a new StringContent object from the JSON string
            var response = await httpClient.PostAsync("http://localhost:3001/results", content); // Send a POST request to the API Controller with the JSON string
            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Movie>(result);
            return response.IsSuccessStatusCode ? obj : null;



        }
        public async Task<List<Movie>?> GetFavoriteMovieList()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://localhost:3001/results");
            var json = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<Movie>>(json) : null;
        }
    }
}
