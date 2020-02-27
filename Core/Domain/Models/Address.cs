using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dotz.Core.Domain.Models
{
  public class Address : IEntity
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 10 e 60 caracteres")]
    [MinLength(10, ErrorMessage = "Este campo deve conter entre 10 e 60 caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 10 e 60 caracteres")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 10 e 60 caracteres")]
    public string City { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(2, ErrorMessage = "Este campo deve conter entre 10 e 2 caracteres")]
    [MinLength(2, ErrorMessage = "Este campo deve conter entre 10 e 2 caracteres")]
    public string State { get; set; }
  }
}