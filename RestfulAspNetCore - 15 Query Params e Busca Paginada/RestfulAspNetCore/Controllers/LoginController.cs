using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace RestfulAspNetCore.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous] // Os usuários precisam pelo menos acessar este método sem login.
        public object Post([FromBody]UsuarioVo usuario)
        {
            if (usuario == null) return BadRequest();

            return _loginService.RetornarPorAcesso(usuario);
        }

    }
}
