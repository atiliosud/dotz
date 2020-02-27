using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dotz.Core.Domain.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Dotz.Services;
using Microsoft.EntityFrameworkCore;
using Dotz.Infra.Data.EFCore;
using Dotz.Core.Domain.Models.Repository;
using Dotz.Infra.Data;
using Microsoft.AspNetCore.Http;

namespace Dotz.Controllers
{
  [Authorize]
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [Produces("application/json")]
  public class AddressController : Controller
  {
    [HttpGet]

    public ActionResult<List<Address>> Get([FromServices] IAddressRepository repository)
    {
      try
      {
        return Ok(repository.Get().AsNoTracking().ToList());
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpGet("{id:int}")]
    public IActionResult Get([FromServices] IAddressRepository repository, int id)
    {
      try
      {
        Address Address = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (Address == null)
          return NotFound(new { message = "Endereço inválido" });
        return Ok(Address);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPost]
    public ActionResult<Address> Create(
        [FromServices] IAddressRepository repository,
        [FromBody]Address model)
    {
      try
      {
        if (ModelState.IsValid){
          repository.Add(model);
          return Ok(new { message = "Endereço de entrega adicionado" });
        }
      
        return BadRequest(ModelState);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPut]
    public ActionResult<Address> Update(
        [FromServices] IAddressRepository repository,
        [FromBody]Address model)
    {
      try
      {
        if (ModelState.IsValid){
          repository.Update(model);
          return Ok(new { message = "Endereço de entrega atualizado" });
        }

        return BadRequest(ModelState);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Address> Delete(
      [FromServices] IAddressRepository repository, int id)
    {
      try
      {
        Address Address = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (Address == null)
        {
          return NotFound(new { message = "Endereço inválido" });
        }
        repository.Delete(Address);
        return Ok(new { message = "Endereço inválido" });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}