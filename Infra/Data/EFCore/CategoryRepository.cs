using System.Collections.Generic;
using System.Linq;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;

namespace Dotz.Infra.Data.EFCore
{
  public class CategoryRepository : Repository<Category>, ICategoryRepository
  {
    public CategoryRepository(DataContext context) : base(context)
    {

    }
  }
}