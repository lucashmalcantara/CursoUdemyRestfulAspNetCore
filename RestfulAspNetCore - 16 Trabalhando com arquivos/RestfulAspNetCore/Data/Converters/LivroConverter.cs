using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Data.Converters
{
    public class LivroConverter : IParser<LivroVo, Livro>, IParser<Livro, LivroVo>
    {
        public Livro Trasformar(LivroVo origem)
        {
            if (origem == null) return null;

            return new Livro
            {
                Id = origem.Id,
                Titulo = origem.Titulo,
                Autor = origem.Autor,
                DataLancamento = origem.DataLancamento,
                Preco = origem.Preco
            };
        }

        public List<Livro> Transformar(List<LivroVo> origem)
        {
            if (origem == null) return null;
            return origem.Select(p => Trasformar(p)).ToList();
        }

        public LivroVo Trasformar(Livro origem)
        {
            if (origem == null) return null;

            return new LivroVo
            {
                Id = origem.Id,
                Titulo = origem.Titulo,
                Autor = origem.Autor,
                DataLancamento = origem.DataLancamento,
                Preco = origem.Preco
            };
        }

        public List<LivroVo> Transformar(List<Livro> origem)
        {
            if (origem == null) return null;
            return origem.Select(p => Trasformar(p)).ToList();
        }
    }
}
