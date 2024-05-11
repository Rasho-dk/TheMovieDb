using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;
using TheMovieDbWebApp;
using TheMovieDbWebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configuration = builder.Configuration; //Configuration is used to access the appsettings.json file
var apiSettings = configuration.GetSection("ApiSettings"); //GetSection is used to access a specific section of the appsettings.json file
//Calling an external API (not in the same URL space as the Client app)
//Link How to call an external API in Blazor WebAssembly: https://learn.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-8.0
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiSettings["BaseUrl"]) };
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiSettings["Token"]);
    return httpClient;
});
builder.Services.AddScoped<IApiService, ApiService>(); //AddScoped is used to create a new instance of the service for each component that needs it

await builder.Build().RunAsync();
