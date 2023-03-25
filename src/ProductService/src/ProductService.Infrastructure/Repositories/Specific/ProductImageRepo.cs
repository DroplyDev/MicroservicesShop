using ProductService.Application.Repositories;
using ProductService.Domain;
using ProductService.Infrastructure.Database;

namespace ProductService.Infrastructure.Repositories.Specific;

public sealed class ProductImageRepo : AppDbRepo<ProductImage>, IProductImageRepo
{
    public ProductImageRepo(AppDbContext context) : base(context)
    {
    }
}
