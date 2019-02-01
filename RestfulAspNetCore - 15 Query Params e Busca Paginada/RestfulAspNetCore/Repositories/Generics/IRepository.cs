using RestfulAspNetCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Repositories.Generics
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Criar(T item);
        T RetornarPorId(long id);
        List<T> RetornarTodos();
        T Atualizar(T item);
        void Excluir(long id);
        bool Existe(long? id);
        List<T> RetornarComBuscaPaginada(string query);
        int RetornarQuantidade(string query);
    }
}
