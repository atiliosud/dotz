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
  public class SubCategoryController : Controller
  {
    [HttpGet]

    public ActionResult<List<SubCategory>> Get([FromServices] ISubCategoryRepository repository)
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

    public IActionResult Get([FromServices] ISubCategoryRepository repository, int id)
    {
      try
      {
        SubCategory SubCategory = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (SubCategory == null)
          return NotFound(new { message = "Categoria inválida" });
        return Ok(SubCategory);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPost]

    public ActionResult<SubCategory> Create(
        [FromServices] ISubCategoryRepository repository,
        [FromBody]SubCategory model)
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

    public ActionResult<SubCategory> Update(
        [FromServices] ISubCategoryRepository repository,
        [FromBody]SubCategory model)
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

    public ActionResult<SubCategory> Delete(
      [FromServices] ISubCategoryRepository repository, int id)
    {
      try
      {
        SubCategory SubCategory = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (SubCategory == null)
        {
          return NotFound(new { message = "Categoria inválida" });
        }
        repository.Delete(SubCategory);
        return Ok(new { message = "Categoria excluida" });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}