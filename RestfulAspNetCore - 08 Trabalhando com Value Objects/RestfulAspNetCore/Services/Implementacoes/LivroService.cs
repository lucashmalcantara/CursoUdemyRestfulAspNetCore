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
    public class LivroService : ILivroService
    {
        private readonly IRepository<Livro> _repository;
        private readonly LivroConverter _converter;

        public LivroService(IRepository<Livro> repository)
        {
            _repository = repository;
            _converter = new LivroConverter();
        }

        public LivroVo Criar(LivroVo pessoa)
        {
            var livroModel = _converter.Trasformar(pessoa);
            livroModel = _repository.Criar(livroModel);
            return _converter.Trasformar(livroModel);
        }

        public LivroVo Atualizar(LivroVo pessoa)
        {
            var livroModel = _converter.Trasformar(pessoa);
            livroModel = _repository.Atualizar(livroModel);
            return _converter.Trasformar(livroModel);
        }

        public void Excluir(long id)
        {
            _repository.Excluir(id);
        }

        public LivroVo RetornarPorId(long id)
        {
            var livroModel = _repository.RetornarPorId(id);
            return _converter.Trasformar(livroModel);
        }

        public List<LivroVo> RetornarTodos()
        {
            var listaLivroModel = _repository.RetornarTodos();
            return _converter.Transformar(listaLivroModel);
        }
    }
}
