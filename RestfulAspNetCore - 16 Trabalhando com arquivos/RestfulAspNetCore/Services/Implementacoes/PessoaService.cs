using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestfulAspNetCore.Data.Converters;
using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Model;
using RestfulAspNetCore.Model.Context;
using RestfulAspNetCore.Repositories;
using RestfulAspNetCore.Repositories.Generics;
using Tapioca.HATEOAS.Utils;

namespace RestfulAspNetCore.Services.Implementacoes
{
    // Responsável pelas regras de negócio e validações.
    public class PessoaService : IPessoaService
    {
        private IPessoaRepository _repository;
        private readonly PessoaConverter _converter;

        public PessoaService(IPessoaRepository repository)
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

        public List<PessoaVo> RetornarPorNome(string nome, string sobrenome)
        {
            var listaPessoaModel = _repository.RetornarPorNome(nome, sobrenome);
            return _converter.Transformar(listaPessoaModel);
        }

        public List<PessoaVo> RetornarTodos()
        {
            var listaPessoaModel = _repository.RetornarTodos();
            return _converter.Transformar(listaPessoaModel);
        }

        public PagedSearchDTO<PessoaVo> RetornarComBuscaPaginada(string nome, string direcaoOrdenacao, int tamanhoPagina, int pagina)
        {
            pagina = pagina > 0 ? pagina - 1 : 0;

            var query = MontarQueryBuscaPaginada(nome, direcaoOrdenacao, tamanhoPagina, pagina);
            var queryQuantidade = MontarQueryTotalBuscaPaginada(nome, direcaoOrdenacao, tamanhoPagina, pagina);

            var listaPessoaModel = _repository.RetornarComBuscaPaginada(query.ToString());
            var listaPessoaVo = _converter.Transformar(listaPessoaModel);
            var totalRegistros = _repository.RetornarQuantidade(queryQuantidade);

            return new PagedSearchDTO<PessoaVo>
            {
                CurrentPage = pagina + 1,
                List = listaPessoaVo,
                PageSize = tamanhoPagina,
                SortDirections = direcaoOrdenacao,
                TotalResults = 10
            };
        }

   
        private string MontarQueryBuscaPaginada(string nome, string direcaoOrdenacao, int tamanhoPagina, int pagina)
        {
            var query = new StringBuilder();

            query.Append("SELECT * FROM Pessoas p");

            if (!string.IsNullOrWhiteSpace(nome)) query.AppendFormat(" WHERE p.nome LIKE '%{0}%'", nome);

            query.AppendFormat(" ORDER BY p.nome {0} LIMIT {1} OFFSET {2};", direcaoOrdenacao, tamanhoPagina, pagina);

            return query.ToString();
        }


        private string MontarQueryTotalBuscaPaginada(string nome, string direcaoOrdenacao, int tamanhoPagina, int pagina)
        {
            var query = new StringBuilder();

            query.Append("SELECT * FROM Pessoas p");

            if (!string.IsNullOrWhiteSpace(nome)) query.AppendFormat(" WHERE p.nome LIKE '%{0}%'", nome);

            return query.ToString();
        }
    }
}
