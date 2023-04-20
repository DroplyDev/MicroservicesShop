using ProductService.Application.Repositories.BaseRepo;
using ProductService.Domain;

namespace ProductService.Application.Repositories;

public interface ICategoryRepo : IBaseGenericRepo<Category>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
