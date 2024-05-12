using System.Runtime.CompilerServices;
using TheMovieDb.Shared.Models;

namespace TheMovieDbWebApp.Services.IService
{
    public interface IApiServiceGetWithParam<T> where T : class 
    {
       
        Task<List<T>> GetAsync(int? newPage = null, string? query = null, int? id = null);
        //Task<List<T>> GetAsync();
    }
}
