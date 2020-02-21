using System;
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

    public List<UserPointsControl> AddBulk(List<UserPointsControl> userPointsControls)
    {
      try
      {
        using (var transaction = _context.Database.BeginTransaction())
        {

          foreach (UserPointsControl obj in userPointsControls)
          {
            _context.Add(obj);
          }

          _context.SaveChanges();

          transaction.Commit();
        }
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException(ex.Message);
      }

      return userPointsControls;
    }
  }
}