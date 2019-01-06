using RestfulAspNetCore.Model;
using System.Collections.Generic;

namespace RestfulAspNetCore.Services
{
    public interface IPessoaService
    {
        Pessoa Criar(Pessoa pessoa);
        Pessoa RetornarPorId(long id);
        List<Pessoa> RetornarTodos();
        Pessoa Atualizar(Pessoa pessoa);
        void Excluir(long id);
    }
}
