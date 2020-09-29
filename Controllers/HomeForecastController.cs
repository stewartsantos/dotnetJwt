using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiJwt.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public Hashtable Index()
        {
            var defaultHath = new Hashtable();
            defaultHath.Add("Mensagem", "Olá bem a minha API do torne-se um programado com JWT");
            return defaultHath;
        }
    }
}
