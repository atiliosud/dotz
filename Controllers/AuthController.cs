using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dotz.Core.Domain.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Dotz.Services;
using Dotz.Core.Domain.Models.Repository;
using Microsoft.AspNetCore.Http;

namespace Dotz.Controllers
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [Produces("application/json")]
  public class AuthController : Controller
  {
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public ActionResult<dynamic> Authenticate([FromBody]User model, [FromServices]IUserRepository repository)
    {
      try
      {
        User user = repository.Get().Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

        if (user == null)
          return NotFound(new { message = "Usuário ou senha inválidos" });

        var token = TokenService.GenerateToken(user);
        user.Password = "";

        return Ok(new
        {
          user = user,
          token = token
        });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    [HttpGet]
    [Route("anonymous")]
    [AllowAnonymous]
    public string Anonymous() => "Anônimo";

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

    [HttpGet]
    [Route("employee")]
    [Authorize(Roles = "employee,manager")]
    public string Employee() => "Funcionário";

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = "manager")]
    public string Manager() => "Gerente";

  }
}