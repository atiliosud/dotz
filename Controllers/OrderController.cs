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
  public class OrderController : Controller
  {
    [HttpGet]

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

    [HttpGet]
    [Route("GetByParameters")]
    /*Listagem de pedidos (com status de entrega) */
    public IActionResult Get([FromServices] IOrderRepository repository, [FromQuery]int? id, [FromQuery] bool? delivered)
    {
      try
      {
        IQueryable<Order> queryOrder = repository.Get()
            .Include(x => x.Address)
            .Include(x => x.User).AsNoTracking();

        if (id.HasValue)
          queryOrder = queryOrder.Where(x => x.Id == id);

        if (delivered.Value)
          queryOrder = queryOrder.Where(x => x.Status == Status.Deliver);

        Order order = queryOrder.FirstOrDefault();

        if (order == null)
          return NotFound(new { message = "Pedido inválido" });

        return Ok(order);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPost]

    public ActionResult<Order> Create(
        [FromServices] IOrderRepository orderRepository,
        [FromServices] IUserPointsControlRepository userPointsRepository,
        [FromServices] IProductRepository productRepository,
        [FromBody]Order model)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var userPoints = UserPointsControlService.GenerateUserPoints(userPointsRepository,productRepository, model);
          if (userPoints.Count > 0)
            userPointsRepository.AddBulk(userPoints);
          orderRepository.Add(model);
          return Ok(new { message = "Pedido criado" });
        }
        else
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