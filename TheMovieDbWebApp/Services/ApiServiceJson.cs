using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using TheMovieDb.Shared.Models;
using TheMovieDbWebApp.Pages;
using TheMovieDbWebApp.Services.IService;
using System.Net.Http;

namespace TheMovieDbWebApp.Services
{
    public class ApiServiceJson : IApiServiceAdd<Movie>, IApiServiceRemove<Movie>
    {
        private HttpClient _httpClient;
        private readonly IConfiguration cnfiguration;

        public ApiServiceJson(HttpClient httpClient, IConfiguration cnfiguration)
        {
            _httpClient = httpClient;
            this.cnfiguration = cnfiguration;
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            var jsonUrl = GetBaseUrl();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(movie); // Serialize the movie object to a JSON string
            var jsonObj = JObject.Parse(json);// Get the JSON object from the JSON string and convert the property name to lowercase.  
            jsonObj["Id"] = Convert.ToString(jsonObj["Id"]); // Convert the Id property to a string
            string updatedJson = jsonObj.ToString();
            json = updatedJson.Replace("Id", jsonObj.Property("Id").Name.ToLower());

            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Create a new StringContent object from the JSON string
            var response = await httpClient.PostAsync($"{jsonUrl}/results", content); // Send a POST request to the API Controller with the JSON string
            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Movie>(result);
            return response.IsSuccessStatusCode ? obj : null;
        }
        public async Task<Movie> RemoveAsync(int id)
        {
            var jsonUrl = GetBaseUrl();

            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(jsonUrl);
                return await _httpClient.DeleteAsync($"/results/{id}")
                   .ContinueWith(response =>
                   {
                       var result =  response.Result.Content.ReadAsStringAsync().Result;
                       var json = JsonConvert.DeserializeObject<Movie>(result);
                       return response.Result.IsSuccessStatusCode ? json : null;
                   });
            }

        }
        private string GetBaseUrl()
        {
            return cnfiguration.GetSection("ApiSettings:JsonUrl").Value;
        }
    }
}
