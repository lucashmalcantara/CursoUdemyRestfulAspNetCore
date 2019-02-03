using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulAspNetCore.Model;
using RestfulAspNetCore.Model.Context;
using RestfulAspNetCore.Repositories.Generics;

namespace RestfulAspNetCore.Repositories.Implementacoes
{
    public class PessoaRepository : GenericRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(MySqlContext context) : base(context)
        {
        }

        public List<Pessoa> RetornarPorNome(string nome, string sobrenome)
        {
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(sobrenome))
            {
                return _context.Pessoas.Where(p => p.Nome.Contains(nome) && p.Sobrenome.Contains(sobrenome)).ToList();
            }
            else if (!string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(sobrenome))
            {
                return _context.Pessoas.Where(p => p.Nome.Contains(nome)).ToList();
            }
            else if (string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(sobrenome))
            {
                return _context.Pessoas.Where(p => p.Sobrenome.Contains(sobrenome)).ToList();
            }
            else
            {
                return _context.Pessoas.ToList();
            }
        }
    }
}
