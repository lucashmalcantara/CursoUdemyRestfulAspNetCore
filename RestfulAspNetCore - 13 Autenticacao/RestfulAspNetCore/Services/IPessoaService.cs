using RestfulAspNetCore.Data.ValueObjects;
using System.Collections.Generic;

namespace RestfulAspNetCore.Services
{
    public interface IPessoaService
    {
        PessoaVo Criar(PessoaVo pessoa);
        PessoaVo RetornarPorId(long id);
        List<PessoaVo> RetornarTodos();
        PessoaVo Atualizar(PessoaVo pessoa);
        void Excluir(long id);
    }
}
