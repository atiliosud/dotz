using System.Collections.Generic;
using System.Linq;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;

namespace Dotz.Infra.Data.EFCore
{
  public class ProductRepository : Repository<Product>, IProductRepository
  {
    public ProductRepository(DataContext context) : base(context)
    {

    }
  }
}