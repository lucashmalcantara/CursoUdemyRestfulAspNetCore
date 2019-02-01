using RestfulAspNetCore.Data.ValueObjects;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestfulAspNetCore.Services
{
    public interface IPessoaService
    {
        PessoaVo Criar(PessoaVo pessoa);
        List<PessoaVo> RetornarPorNome(string nome, string sobrenome);
        PessoaVo RetornarPorId(long id);
        List<PessoaVo> RetornarTodos();
        PessoaVo Atualizar(PessoaVo pessoa);
        void Excluir(long id);
        PagedSearchDTO<PessoaVo> RetornarComBuscaPaginada(string nome, string direcaoOrdenacao, int tamanhoPagina, int pagina);
    }
}
