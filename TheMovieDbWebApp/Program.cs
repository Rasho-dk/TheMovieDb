using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;
using TheMovieDb.Shared.Models;
using TheMovieDbWebApp;
using TheMovieDbWebApp.Services;
using TheMovieDbWebApp.Services.IService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var configuration = builder.Configuration;
var apiSettings = configuration.GetSection("ApiSettings");


builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiSettings["BaseUrl"]) };
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiSettings["Token"]);
    return httpClient;
});
//builder.Services.AddScoped<IApiService, ApiService>(); //AddScoped is used to create a new instance of the service for each component that needs it
builder.Services.AddScoped<IApiServiceGetWithParam<Movie>, ApiService_>();
builder.Services.AddScoped<IApiServiceGet<Movie>, ApiService_>();
builder.Services.AddScoped<IApiServiceRemove<Movie>, ApiServiceJson>();
builder.Services.AddScoped<IApiServiceAdd<Movie>, ApiServiceJson>();

builder.Services.AddScoped<IApiServiceGetWithParam<MovieVideo>, ApiServiceMovieVideo>();

builder.Services.AddScoped<IApiServiceGet<Genre>, ApiServiceGenre>();


await builder.Build().RunAsync();
