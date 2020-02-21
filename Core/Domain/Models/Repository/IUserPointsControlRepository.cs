

using System.Collections.Generic;

namespace Dotz.Core.Domain.Models.Repository
{
  public interface IUserPointsControlRepository : IRepository<UserPointsControl>
  {
    List<UserPointsControl> AddBulk(List<UserPointsControl> userPointsControls);
  }
}