
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dotz.Core.Domain.Models
{
  public class Order : IEntity
  {
    [Key]
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public Status Status { get; set; }
    public decimal Total { get; set; }
    public UserPointsControl UserPointsControl { get; set; }
  }

  public enum Status
  {
    Deliver,
    Pending,
    Created
  }
}