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
  [Route("api/v{version:apiVersion}/categories")]
  [Produces("application/json")]
  public class OrderController : Controller
  {
    [HttpGet]
    [Route("")]
    public ActionResult<List<Order>> Get([FromServices] IOrderRepository repository)
    {
      try
      {
        return Ok(repository.Get().ToList());
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpGet("{id:int}")]
    public IActionResult Get([FromServices] IOrderRepository repository, int id)
    {
      try
      {
        Order Order = repository.Get()
            .Include(x => x.Address)
            .Include(x => x.User).AsNoTracking().FirstOrDefault(x => x.Id == id);

        if (Order == null)
          return NotFound(new { message = "Pedido inválido" });
        return Ok(Order);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPost]
    public ActionResult<Order> Create(
        [FromServices] IOrderRepository repository,
        [FromBody]Order model)
    {
      try
      {
        if (ModelState.IsValid)
          return Ok(repository.Add(model));

        return BadRequest(ModelState);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPut]
    public ActionResult<Order> Update(
        [FromServices] IOrderRepository repository,
        [FromBody]Order model)
    {
      try
      {
        if (ModelState.IsValid)
          return Ok(repository.Update(model));

        return BadRequest(ModelState);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Order> Delete(
      [FromServices] IOrderRepository repository, int id)
    {
      try
      {
        Order Order = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (Order == null)
        {
          return NotFound(new { message = "Pedido inválido" });
        }
        repository.Delete(Order);
        return Ok(new { message = "Pedido excluido" });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}