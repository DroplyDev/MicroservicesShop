using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Application.Repositories;
using ProductService.Domain;
using ProductService.Infrastructure.Database;
using ProductService.Infrastructure.Repositories.Base;

namespace ProductService.Infrastructure.Repositories.Specific;
public sealed class ProductImageRepo : AppDbRepo<ProductImage>, IProductImageRepo
{
	public ProductImageRepo(AppDbContext context) : base(context)
	{
	}
}
