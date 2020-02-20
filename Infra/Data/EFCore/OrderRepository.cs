using System.Collections.Generic;
using System.Linq;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;

namespace Dotz.Infra.Data.EFCore
{
  public class OrderRepository : Repository<Order>, IOrderRepository
  {
    public OrderRepository(DataContext context) : base(context)
    {

    }
  }
}