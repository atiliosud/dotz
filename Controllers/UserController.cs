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
  [Route("api/v{version:apiVersion}/users")]
  [Produces("application/json")]
  public class UserController : Controller
  {
    [HttpGet]
    [Route("")]
    public ActionResult<List<User>> Get([FromServices] IUserRepository repository)
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
    public IActionResult Get([FromServices] IUserRepository repository, int id)
    {
      try
      {
        User User = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (User == null)
          return NotFound(new { message = "Usuário inválido" });
        return Ok(User);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPost]
    public ActionResult<User> Create(
        [FromServices] IUserRepository repository,
        [FromBody]User model)
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
    public ActionResult<User> Update(
        [FromServices] IUserRepository repository,
        [FromBody]User model)
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
    public ActionResult<User> Delete(
      [FromServices] IUserRepository repository, int id)
    {
      try
      {
        User User = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (User == null)
        {
          return NotFound(new { message = "Usuário inválido" });
        }
        repository.Delete(User);
        return Ok(new { message = "Usuário excluido" });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}