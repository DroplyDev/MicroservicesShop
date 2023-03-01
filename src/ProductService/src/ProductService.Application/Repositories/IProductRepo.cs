using ProductService.Application.Repositories.BaseRepo;
using ProductService.Domain;

namespace ProductService.Application.Repositories;

public interface IProductRepo : IBaseRepo<Product>
{
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}