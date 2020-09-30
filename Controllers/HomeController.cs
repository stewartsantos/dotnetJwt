using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJwt.Models;
using ApiJwt.Repositories;
using ApiJwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiJwt.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public Hashtable Index()
        {
            var defaultHath = new Hashtable();
            defaultHath.Add("Mensagem", "Olá bem a minha API do torne-se um programado com JWT");
            return defaultHath;
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody]User model)
        {
            try
            {
                var service = new UserService();
                var user = service.Authenticate(model.Username, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Usuário ou senha inválidos" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("/users")]
        [Authorize(Roles = "administrador")]
        public IList<User> Get() => UserRepository.All();

        [HttpGet]
        [Route("/users/one")]
        [Authorize(Roles = "editor, administrador")]
        public User Editor() => UserRepository.All().First();
    }
}
