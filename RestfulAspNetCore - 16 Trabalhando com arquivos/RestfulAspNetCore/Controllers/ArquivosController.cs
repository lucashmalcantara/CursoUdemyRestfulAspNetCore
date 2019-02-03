using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Security.Configurations;
using RestfulAspNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace RestfulAspNetCore.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ArquivosController : Controller
    {
        private readonly IArquivoService _arquivoService;

        public ArquivosController(IArquivoService loginService)
        {
            _arquivoService = loginService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(byte[]))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize(SigningConfigurations.MODO_AUTORIZACAO)]
        public IActionResult Get()
        {
            var buffer = _arquivoService.RetornarArquivPdf();

            if (buffer == null)
                return NoContent();

            HttpContext.Response.ContentType = MediaTypeNames.Application.Pdf;
            HttpContext.Response.Headers.Add(HeaderNames.ContentLength, buffer.Length.ToString());
            HttpContext.Response.Body.Write(buffer);

            return new ContentResult();
        }

    }
}
