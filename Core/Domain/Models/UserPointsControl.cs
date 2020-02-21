
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
    public int GeneratedPoints { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public bool CanDischarge { get; set; }
  }
}