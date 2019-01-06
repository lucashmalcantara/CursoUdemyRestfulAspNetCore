using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Model;
using RestfulAspNetCore.Services;

namespace RestfulAspNetCore.Controllers
{
    // Utilizando a arquitetura adotada neste projeto:
    // Controller: vai rotear a requisição para o método correto.
    // Services: vai validar e conter as regras de negócio.
    // Repositories: acesso aos dados e persistência.
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PessoasController : Controller
    {
        private IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pessoaService.RetornarTodos());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pessoa = _pessoaService.RetornarPorId(id);

            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();

            return new ObjectResult(
                _pessoaService.Criar(pessoa));
        }

        // PUT api/values/
        [HttpPut]
        public IActionResult Put([FromBody]Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();

            var pessoaAtualizada = _pessoaService.Atualizar(pessoa);

            if (pessoa == null) return NoContent();

            return new ObjectResult(pessoaAtualizada);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _pessoaService.Excluir(id);
            return NoContent();
        }
    }
}
