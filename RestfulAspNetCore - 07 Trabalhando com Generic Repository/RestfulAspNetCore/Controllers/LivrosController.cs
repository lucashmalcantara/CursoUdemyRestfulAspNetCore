using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Model;
using RestfulAspNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Get()
        {
            return Ok(_livroService.RetornarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var livro = _livroService.RetornarPorId(id);

            if (livro == null) return NotFound();

            return Ok(livro);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Livro livro)
        {
            if (livro == null) return BadRequest();

            return new ObjectResult(
                _livroService.Criar(livro));
        }

        [HttpPut]
        public IActionResult Put([FromBody]Livro livro)
        {
            if (livro == null) return BadRequest();

            var livroAtualizado = _livroService.Atualizar(livro);

            if (livroAtualizado == null) return NoContent();

            return new ObjectResult(livroAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _livroService.Excluir(id);
            return NoContent();
        }
    }
}
