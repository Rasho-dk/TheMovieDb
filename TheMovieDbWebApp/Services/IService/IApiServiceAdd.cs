namespace TheMovieDbWebApp.Services.IService
{
    public interface IApiServiceAdd<T> where T : class
    {
        Task<T> AddAsync(T entity);
    }
}
