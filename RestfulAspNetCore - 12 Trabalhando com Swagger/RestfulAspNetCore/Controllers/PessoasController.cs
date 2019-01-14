using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<PessoaVo>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        // Adiciona o HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_pessoaService.RetornarTodos());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PessoaVo))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var pessoa = _pessoaService.RetornarPorId(id);

            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(PessoaVo))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]PessoaVo pessoa)
        {
            if (pessoa == null) return BadRequest();

            return new ObjectResult(
                _pessoaService.Criar(pessoa));
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Accepted, Type = typeof(List<PessoaVo>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody]PessoaVo pessoa)
        {
            if (pessoa == null) return BadRequest();

            var pessoaAtualizada = _pessoaService.Atualizar(pessoa);

            if (pessoaAtualizada == null) return NoContent();

            return new ObjectResult(pessoaAtualizada);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _pessoaService.Excluir(id);
            return NoContent();
        }
    }
}
