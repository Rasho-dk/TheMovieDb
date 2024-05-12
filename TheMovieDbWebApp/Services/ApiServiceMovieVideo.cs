using Newtonsoft.Json;
using System.Web.Http;
using TheMovieDb.Shared.Models;
using TheMovieDbWebApp.Services.IService;

namespace TheMovieDbWebApp.Services
{
    public class ApiServiceMovieVideo : IApiServiceGetWithParam<MovieVideo>
    {
        private readonly HttpClient _httpClient;

        public ApiServiceMovieVideo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<MovieVideo>> GetAsync(int? newPage = null, string? query = null, int? id = null)
        {
            if (id.HasValue)
            {
                var response = await _httpClient.GetAsync($"movie/{id}/videos");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MovieVideoResponse>(json);
                    return result.Results;
                }
                else
                {
                    throw new HttpResponseException(response);
                }
            }
            else
            {
                return null;
            }
        }

        public Task<List<MovieVideo>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
