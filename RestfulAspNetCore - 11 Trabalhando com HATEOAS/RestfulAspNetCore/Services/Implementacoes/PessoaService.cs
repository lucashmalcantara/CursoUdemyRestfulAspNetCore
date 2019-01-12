using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestfulAspNetCore.Data.Converters;
using RestfulAspNetCore.Data.ValueObjects;
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
        private readonly PessoaConverter _converter;

        public PessoaService(IRepository<Pessoa> repository)
        {
            _repository = repository;
            _converter = new PessoaConverter();
        }

        public PessoaVo Criar(PessoaVo pessoa)
        {
            var pessoaModel = _converter.Trasformar(pessoa);
            pessoaModel = _repository.Criar(pessoaModel);
            return _converter.Trasformar(pessoaModel);
        }

        public PessoaVo Atualizar(PessoaVo pessoa)
        {
            var pessoaModel = _converter.Trasformar(pessoa);
            pessoaModel = _repository.Atualizar(pessoaModel);
            return _converter.Trasformar(pessoaModel);
        }

        public void Excluir(long id)
        {
            _repository.Excluir(id);
        }

        public PessoaVo RetornarPorId(long id)
        {
            var pessoaModel = _repository.RetornarPorId(id);
            return _converter.Trasformar(pessoaModel);
        }

        public List<PessoaVo> RetornarTodos()
        {
            var listaPessoaModel = _repository.RetornarTodos();
            return _converter.Transformar(listaPessoaModel);
        }
    }
}
