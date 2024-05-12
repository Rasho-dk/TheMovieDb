namespace TheMovieDbWebApp.Services.IService
{
    public interface IApiServiceGet<T> where T : class
    {
        Task<List<T>> GetAsync();
    }
}
