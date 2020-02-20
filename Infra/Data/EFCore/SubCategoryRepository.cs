using System.Collections.Generic;
using System.Linq;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;

namespace Dotz.Infra.Data.EFCore
{
  public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
  {
    public SubCategoryRepository(DataContext context) : base(context)
    {

    }
  }
}