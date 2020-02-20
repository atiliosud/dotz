using System.Collections.Generic;
using System.Linq;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;

namespace Dotz.Infra.Data.EFCore
{
  public class AddressRepository : Repository<Address>, IAddressRepository
  {
    public AddressRepository(DataContext context) : base(context)
    {

    }
  }
}