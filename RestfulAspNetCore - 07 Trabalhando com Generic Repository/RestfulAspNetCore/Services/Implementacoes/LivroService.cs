using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestfulAspNetCore.Model;
using RestfulAspNetCore.Model.Context;
using RestfulAspNetCore.Repositories;
using RestfulAspNetCore.Repositories.Generics;

namespace RestfulAspNetCore.Services.Implementacoes
{
    // Responsável pelas regras de negócio e validações.
    public class LivroService : ILivroService
    {
        private readonly IRepository<Livro> _repository;

        public LivroService(IRepository<Livro> repository)
        {
            _repository = repository;
        }

        public Livro Criar(Livro livro)
        {
            return _repository.Criar(livro);
        }

        public Livro Atualizar(Livro livro)
        {
            return _repository.Atualizar(livro);
        }

        public void Excluir(long id)
        {
            _repository.Excluir(id);
        }

        public Livro RetornarPorId(long id)
        {
            return _repository.RetornarPorId(id);
        }

        public List<Livro> RetornarTodos()
        {
            return _repository.RetornarTodos();
        }
    }
}
