using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dotz.Core.Domain.Models
{
  public class Address : IEntity
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 10 e 300 caracteres")]
    [MinLength(10, ErrorMessage = "Este campo deve conter entre 10 e 300 caracteres")]
    public string Name { get; set; }
  }
}