using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Security.Configurations;
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
        [Authorize(SigningConfigurations.MODO_AUTORIZACAO)]
        public IActionResult Get()
        {
            return Ok(_pessoaService.RetornarTodos());
        }

        [HttpGet("RetornarPorNome")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<PessoaVo>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        // Adiciona o HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize(SigningConfigurations.MODO_AUTORIZACAO)]
        public IActionResult Get([FromQuery] string nome, [FromQuery] string sobrenome) // Query params são opcionais.
        {
            return Ok(_pessoaService.RetornarPorNome(nome, sobrenome));
        }

        //public PagedSearchDTO<PessoaVo> RetornarComBuscaPaginada(string nome, string direcaoOrdenacao, int tamanhoPagina, int pagina)
        [HttpGet("RetornarComBuscaPaginada/{direcaoOrdenacao}/{tamanhoPagina}/{pagina}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<PessoaVo>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        // Adiciona o HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize(SigningConfigurations.MODO_AUTORIZACAO)]
        public IActionResult Get([FromQuery] string nome, [FromRoute]string direcaoOrdenacao, [FromRoute]int tamanhoPagina, [FromRoute]int pagina)
        {
            return Ok(_pessoaService.RetornarComBuscaPaginada(nome, direcaoOrdenacao, tamanhoPagina, pagina));
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PessoaVo))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize(SigningConfigurations.MODO_AUTORIZACAO)]
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
        [Authorize(SigningConfigurations.MODO_AUTORIZACAO)]
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
        [Authorize(SigningConfigurations.MODO_AUTORIZACAO)]
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
        [Authorize(SigningConfigurations.MODO_AUTORIZACAO)]
        public IActionResult Delete(int id)
        {
            _pessoaService.Excluir(id);
            return NoContent();
        }
    }
}
