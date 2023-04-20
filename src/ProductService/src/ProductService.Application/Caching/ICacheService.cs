namespace ProductService.Application.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey);
    Task SetAsync<T>(string cacheKey, T value);
    Task RemoveAsync(string cacheKey);
}
