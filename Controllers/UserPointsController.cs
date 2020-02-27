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
  public class UserPointsController : Controller
  {
    [HttpGet]
    public ActionResult<List<UserPointsControl>> Get([FromServices] IUserPointsControlRepository repository)
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

    [HttpGet]
    [Route("GetByParameters")]
    public IActionResult Get([FromServices] IUserPointsControlRepository repository,
              [FromQuery] int? id, [FromQuery] int? productId, [FromQuery] int? userId, [FromQuery] bool? pointsBalance)
    {
      try
      {
        IQueryable<UserPointsControl> queryUserPoints = repository.Get()
            .Include(x => x.Product)
            .Include(x => x.User).AsNoTracking();

        if (id.HasValue)
          queryUserPoints = queryUserPoints.Where(x => x.Id == id);

        if (productId.HasValue)
          queryUserPoints = queryUserPoints.Where(x => x.ProductId == productId);

        if (userId.HasValue)
          queryUserPoints = queryUserPoints.Where(x => x.UserId == userId);

        List<UserPointsControl> listUserPoints = queryUserPoints.ToList();

        int pointsBalanceTotal = 0;
        if (pointsBalance.HasValue){
            pointsBalanceTotal = listUserPoints.Sum(x => x.GeneratedPoints);
            return Ok(pointsBalanceTotal);
        }

        if (listUserPoints == null || listUserPoints.Count == 0)
          return NotFound(new { message = "O usuário não tem pontos utilizados para o produto" });

        return Ok(listUserPoints);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPost]
    public ActionResult<UserPointsControl> Create(
        [FromServices] IUserPointsControlRepository repository,
        [FromBody]UserPointsControl model)
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
    public ActionResult<UserPointsControl> Update(
        [FromServices] IUserPointsControlRepository repository,
        [FromBody]UserPointsControl model)
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

    public ActionResult<UserPointsControl> Delete(
      [FromServices] IUserPointsControlRepository repository, int id)
    {
      try
      {
        UserPointsControl UserPoints = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (UserPoints == null)
        {
          return NotFound(new { message = "Categoria inválida" });
        }
        repository.Delete(UserPoints);
        return Ok(new { message = "Categoria excluida" });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}