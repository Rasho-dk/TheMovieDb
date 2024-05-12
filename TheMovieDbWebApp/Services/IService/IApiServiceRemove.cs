namespace TheMovieDbWebApp.Services.IService
{
    public interface IApiServiceRemove<T> where T : class
    {
        Task<T> RemoveAsync(int id);
    }
}
