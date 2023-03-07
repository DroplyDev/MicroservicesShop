using ProductService.Application.Repositories;
using ProductService.Domain;
using ProductService.Infrastructure.Database;

namespace ProductService.Infrastructure.Repositories.Specific;

public sealed class ProductRepo : AppDbRepo<Product>, IProductRepo
{
	public ProductRepo(AppDbContext context) : base(context)
	{
	}

	public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
	{
		return await FirstOrDefaultAsync(item => item.Name == name, cancellationToken);
	}
}