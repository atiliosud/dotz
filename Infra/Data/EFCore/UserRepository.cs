using System.Collections.Generic;
using System.Linq;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;

namespace Dotz.Infra.Data.EFCore
{
  public class UserRepository : Repository<User>, IUserRepository
  {
    public UserRepository(DataContext context) : base(context)
    {

    }
  }
}