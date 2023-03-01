using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Application.Repositories.BaseRepo;
using ProductService.Domain;

namespace ProductService.Application.Repositories;
public interface IProductImageRepo : IBaseRepo<ProductImage>
{

}
