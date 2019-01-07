using RestfulAspNetCore.Model;
using System.Collections.Generic;

namespace RestfulAspNetCore.Services
{
    public interface ILivroService
    {
        Livro Criar(Livro livro);
        Livro RetornarPorId(long id);
        List<Livro> RetornarTodos();
        Livro Atualizar(Livro livro);
        void Excluir(long id);
    }
}
