using TheMovieDb.Shared.Models;

namespace TheMovieDbWebApp.Services
{
    public interface IApiService
    {
        Task<List<Movie>> GetAll(int? newPage = null);
        Task<List<Movie>> SearchMovie(string query);
        Task<List<MovieVideo>> GetMovieVideo(int id);
        Task<Movie> AddMovie(Movie movie);
        Task<List<Movie?>> GetFavoriteMovieList();
    }
}
