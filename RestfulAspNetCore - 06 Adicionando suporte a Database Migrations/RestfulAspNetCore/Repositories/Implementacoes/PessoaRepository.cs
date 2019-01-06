using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestfulAspNetCore.Model;
using RestfulAspNetCore.Model.Context;

namespace RestfulAspNetCore.Repositories.Implementacoes
{
    public class PessoaRepository : IPessoaRepository
    {
        private MySqlContext _context;

        public PessoaRepository(MySqlContext context)
        {
            _context = context;
        }

        public Pessoa Atualizar(Pessoa pessoa)
        {
            if (!Existe(pessoa.Id)) return null;

            var pessoaEncontrada = _context.Pessoas.SingleOrDefault(p => p.Id.Equals(pessoa.Id));

            try
            {
                _context.Entry(pessoaEncontrada).CurrentValues.SetValues(pessoa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pessoa;
        }

        public bool Existe(long? id)
        {
            return _context.Pessoas.Any(p => p.Id.Equals(id));
        }

        public Pessoa Criar(Pessoa pessoa)
        {
            try
            {
                _context.Add(pessoa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pessoa;
        }

        public void Excluir(long id)
        {
            var pessoaEncontrada = _context.Pessoas.SingleOrDefault(p => p.Id.Equals(id));

            try
            {
                if (pessoaEncontrada == null)
                    return;

                _context.Pessoas.Remove(pessoaEncontrada);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Pessoa RetornarPorId(long id)
        {
            return _context.Pessoas.FirstOrDefault(p => p.Id.Equals(id));
        }

        public List<Pessoa> RetornarTodos()
        {
            return _context.Pessoas.ToList();
        }
    }
}
