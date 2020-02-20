using System.Collections.Generic;
using System.Linq;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;

namespace Dotz.Infra.Data.EFCore
{
  public class UserPointsControlRepository : Repository<UserPointsControl>, IUserPointsControlRepository
  {
    public UserPointsControlRepository(DataContext context) : base(context)
    {

    }
  }
}