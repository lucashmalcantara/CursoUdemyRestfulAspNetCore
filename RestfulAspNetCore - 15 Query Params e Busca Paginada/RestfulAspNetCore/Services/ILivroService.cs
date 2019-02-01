using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Model;
using System.Collections.Generic;

namespace RestfulAspNetCore.Services
{
    public interface ILivroService
    {
        LivroVo Criar(LivroVo livro);
        LivroVo RetornarPorId(long id);
        List<LivroVo> RetornarTodos();
        LivroVo Atualizar(LivroVo livro);
        void Excluir(long id);
    }
}
