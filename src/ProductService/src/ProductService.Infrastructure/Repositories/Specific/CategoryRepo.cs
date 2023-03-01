using ProductService.Application.Repositories;
using ProductService.Domain;
using ProductService.Infrastructure.Database;

namespace ProductService.Infrastructure.Repositories.Specific;

public class CategoryRepo : AppDbRepo<Category>, ICategoryRepo
{
    public CategoryRepo(AppDbContext context) : base(context)
    {
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await FirstOrDefaultAsync(item => item.Name == name, cancellationToken);
    }
}