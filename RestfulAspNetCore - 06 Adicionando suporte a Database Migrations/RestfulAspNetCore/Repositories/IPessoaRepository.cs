using RestfulAspNetCore.Model;
using System.Collections.Generic;

namespace RestfulAspNetCore.Repositories
{
    public interface IPessoaRepository
    {
        Pessoa Criar(Pessoa pessoa);
        Pessoa RetornarPorId(long id);
        List<Pessoa> RetornarTodos();
        Pessoa Atualizar(Pessoa pessoa);
        void Excluir(long id);
        bool Existe(long? id);
    }
}
