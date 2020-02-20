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
  public class CategoryController : Controller
  {
    [HttpGet]
    public ActionResult<List<Category>> Get([FromServices] ICategoryRepository repository)
    {
      try
      {
        return Ok(repository.Get().Include(x => x.SubCategory).AsNoTracking().ToList());
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpGet("{id:int}")]
    public IActionResult Get([FromServices] ICategoryRepository repository, int id)
    {
      try
      {
        Category category = repository.Get().Include(x => x.SubCategory).AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (category == null)
          return NotFound(new { message = "Categoria inválida" });
        return Ok(category);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPost]
    public ActionResult<Category> Create(
        [FromServices] ICategoryRepository repository,
        [FromBody]Category model)
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
    public ActionResult<Category> Update(
        [FromServices] ICategoryRepository repository,
        [FromBody]Category model)
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

    public ActionResult<Category> Delete(
      [FromServices] ICategoryRepository repository, int id)
    {
      try
      {
        Category category = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (category == null)
        {
          return NotFound(new { message = "Categoria inválida" });
        }
        repository.Delete(category);
        return Ok(new { message = "Categoria excluida" });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}