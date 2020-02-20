
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dotz.Core.Domain.Models
{
  public class UserPointsControl : IEntity
  {
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int AccumulatedPoints { get; set; }
    public List<Product> ProductsToDischarge { get; set; }
    public List<Order> Orders { get; set; }
  }
}