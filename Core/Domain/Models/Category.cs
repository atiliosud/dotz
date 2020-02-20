using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dotz.Core.Domain.Models
{
  public class Category : IEntity
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
    public string Title { get; set; }
    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
  }
}