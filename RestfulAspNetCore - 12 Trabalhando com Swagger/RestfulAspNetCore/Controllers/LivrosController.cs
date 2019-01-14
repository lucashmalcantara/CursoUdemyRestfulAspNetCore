using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Services;
using System.Collections.Generic;
using System.Net;
using Tapioca.HATEOAS;

namespace RestfulAspNetCore.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LivrosController : Controller
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<LivroVo>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_livroService.RetornarTodos());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LivroVo))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var livro = _livroService.RetornarPorId(id);

            if (livro == null) return NotFound();

            return Ok(livro);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(LivroVo))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]LivroVo livro)
        {
            if (livro == null) return BadRequest();

            return new ObjectResult(
                _livroService.Criar(livro));
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Accepted, Type = typeof(List<LivroVo>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody]LivroVo livro)
        {
            if (livro == null) return BadRequest();

            var livroAtualizado = _livroService.Atualizar(livro);

            if (livroAtualizado == null) return NoContent();

            return new ObjectResult(livroAtualizado);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _livroService.Excluir(id);
            return NoContent();
        }
    }
}
