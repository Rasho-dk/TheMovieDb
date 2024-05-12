using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Helpers;
using TheMovieDb.Shared.Models;
using NetJSON;
using static TheMovieDb.Shared.Models.MovieVideo;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Web.Http;
using TheMovieDbWebApp.Services.IService;
using Microsoft.Extensions.Options;

namespace TheMovieDbWebApp.Services
{
    public class ApiService_ : IApiServiceGetWithParam<Movie>, IApiServiceGet<Movie>
    {
        private HttpClient _httpClient;
        private readonly IConfiguration Configuration;

        public ApiService_(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            Configuration = configuration;
        }

        public async Task<List<Movie>> GetAsync(int? newPage = null, string? query = null, int? id = null)
        {
            if (query != null)
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
                    throw new HttpResponseException(response);
                    //return null;
                }
            }
            if (id.HasValue)
            {
                var response = await _httpClient.GetAsync(newPage.HasValue ? $"discover/movie?page={newPage}&with_genres={id}" : $"discover/movie?with_genres={id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MovieResponse>(json);
                    return result.Results;
                }
                else
                {
                    throw new HttpResponseException(response);
                }
            }
            if (newPage.HasValue || newPage is null)
            {
                var response = await _httpClient.GetAsync(newPage.HasValue ? $"discover/movie?page={newPage}" : "discover/movie");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MovieResponse>(json);
                    return result.Results;
                }
                else
                {
                    throw new HttpResponseException(response);
                }

            }
            
            return null;
        }

        /// <summary>
        /// This method gets all the movies from JSON Server for a favorite movie list
        /// </summary>
        /// <returns>A List of the favorite moive list</returns>
        /// <exception cref="HttpResponseException"></exception>
        public async Task<List<Movie>> GetAsync()
        {
            var jsonUrl = GetBaseUrl();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(jsonUrl);
                var response = await _httpClient.GetAsync("/results");
                var json = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<Movie>>(json) : throw new HttpResponseException(response);
            }

        }
        private string GetBaseUrl()
        {
            return Configuration.GetSection("ApiSettings:JsonUrl").Value;
        }
    }

}

