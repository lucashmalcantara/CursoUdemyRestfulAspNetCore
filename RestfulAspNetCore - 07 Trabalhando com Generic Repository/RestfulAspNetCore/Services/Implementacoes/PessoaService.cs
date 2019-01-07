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
    public class PessoaService : IPessoaService
    {
        private IRepository<Pessoa> _repository;

        public PessoaService(IRepository<Pessoa> repository)
        {
            _repository = repository;
        }

        public Pessoa Criar(Pessoa pessoa)
        {
            return _repository.Criar(pessoa);
        }

        public Pessoa Atualizar(Pessoa pessoa)
        {
            return _repository.Atualizar(pessoa);
        }

        public void Excluir(long id)
        {
            _repository.Excluir(id);
        }

        public Pessoa RetornarPorId(long id)
        {
            return _repository.RetornarPorId(id);
        }

        public List<Pessoa> RetornarTodos()
        {
            return _repository.RetornarTodos();
        }
    }
}
