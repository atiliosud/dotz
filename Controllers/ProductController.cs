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
  public class ProductController : Controller
  {
    [HttpGet]

    public ActionResult<List<Product>> Get([FromServices] IProductRepository repository)
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
    public IActionResult Get([FromServices] IProductRepository repository, [FromQuery] int? id, [FromQuery] bool? avaiableToDischarge)
    {
      try
      {
        IQueryable<Product> queryProducts = repository.Get()
            .AsNoTracking();

        if (id.HasValue)
          queryProducts = queryProducts.Where(x => x.Id == id);

        if (avaiableToDischarge.HasValue)
          queryProducts = queryProducts.Where(x => x.AvaiableToDischarge == avaiableToDischarge.Value);

       List<Product> listProducts = queryProducts.ToList();

        if (listProducts == null || listProducts.Count ==0)
          return NotFound(new { message = "Não existem produtos disponiveis para resgate" });

        return Ok(listProducts);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPost]

    public ActionResult<Product> Create(
        [FromServices] IProductRepository repository,
        [FromBody]Product model)
    {
      try
      {
        if (ModelState.IsValid){
          repository.Add(model);
          return Ok(new { message = "Produto adicionado com sucesso" });
        }

        return BadRequest(ModelState);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpPut]

    public ActionResult<Product> Update(
        [FromServices] IProductRepository repository,
        [FromBody]Product model)
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

    public ActionResult<Product> Delete(
      [FromServices] IProductRepository repository, int id)
    {
      try
      {
        Product Product = repository.Get().AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (Product == null)
        {
          return NotFound(new { message = "Produto inválido" });
        }
        repository.Delete(Product);
        return Ok(new { message = "Produto excluido" });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }
  }
}