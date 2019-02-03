using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Data.Converters
{
    public class PessoaConverter : IParser<PessoaVo, Pessoa>, IParser<Pessoa, PessoaVo>
    {
        public Pessoa Trasformar(PessoaVo origem)
        {
            if (origem == null) return null;

            return new Pessoa
            {
                Id = origem.Id,
                Nome = origem.Nome,
                Sobrenome = origem.Sobrenome,
                Endereco = origem.Endereco,
                Genero = origem.Genero
            };
        }

        public List<Pessoa> Transformar(List<PessoaVo> origem)
        {
            if (origem == null) return null;
            return origem.Select(p => Trasformar(p)).ToList();
        }

        public PessoaVo Trasformar(Pessoa origem)
        {
            if (origem == null) return null;

            return new PessoaVo
            {
                Id = origem.Id,
                Nome = origem.Nome,
                Sobrenome = origem.Sobrenome,
                Endereco = origem.Endereco,
                Genero = origem.Genero
            };
        }

        public List<PessoaVo> Transformar(List<Pessoa> origem)
        {
            if (origem == null) return null;
            return origem.Select(p => Trasformar(p)).ToList();
        }
    }
}
