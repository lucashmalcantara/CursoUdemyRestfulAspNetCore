using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Services;
using Tapioca.HATEOAS;

namespace RestfulAspNetCore.Controllers
{
    // Utilizando a arquitetura adotada neste projeto:
    // Controller: vai rotear a requisição para o método correto.
    // Services: vai validar e conter as regras de negócios.
    // Repositories: acesso aos dados e persistência.
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PessoasController : Controller
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        // Adiciona o HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_pessoaService.RetornarTodos());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var pessoa = _pessoaService.RetornarPorId(id);

            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]PessoaVo pessoa)
        {
            if (pessoa == null) return BadRequest();

            return new ObjectResult(
                _pessoaService.Criar(pessoa));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody]PessoaVo pessoa)
        {
            if (pessoa == null) return BadRequest();

            var pessoaAtualizada = _pessoaService.Atualizar(pessoa);

            if (pessoaAtualizada == null) return NoContent();

            return new ObjectResult(pessoaAtualizada);
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _pessoaService.Excluir(id);
            return NoContent();
        }
    }
}
