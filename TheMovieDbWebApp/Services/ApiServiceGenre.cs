using Newtonsoft.Json;
using System.Web.Http;
using TheMovieDb.Shared.Models;
using TheMovieDbWebApp.Services.IService;

namespace TheMovieDbWebApp.Services
{
    public class ApiServiceGenre : IApiServiceGet<Genre>
    {
        private readonly HttpClient _httpClient;
        public ApiServiceGenre(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Genre>> GetAsync()
        {
            var response = await _httpClient.GetAsync("genre/movie/list");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GenreResponse>(json);
                return result.Genres;
            }
            else
            {
                throw new HttpResponseException(response);
            }

        }
    }
}
