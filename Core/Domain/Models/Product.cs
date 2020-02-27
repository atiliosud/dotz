using System.ComponentModel.DataAnnotations;

namespace Dotz.Core.Domain.Models
{
  public class Product : IEntity
  {
    [Key]
    public int Id { get; set; }

    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
    public string Title { get; set; }

    [MaxLength(1024, ErrorMessage = "Este campo deve conter no máximo 1024 caracteres")]
    public string Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int PointsToAdd { get; set; }
    public int PointsToDischarge { get; set; }
    public bool AvaiableToDischarge { get; set; }
  }
}