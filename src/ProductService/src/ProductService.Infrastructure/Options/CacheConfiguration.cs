namespace ProductService.Infrastructure.Options;

public class CacheConfiguration
{
    public TimeOnly AbsoluteExpirationLifetime { get; set; }
    public TimeOnly? SlidingExpirationLifetime { get; set; }
}
