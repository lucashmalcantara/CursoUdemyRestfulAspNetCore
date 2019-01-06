using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestfulAspNetCore.Model;

namespace RestfulAspNetCore.Services.Implementacoes
{
    public class PessoaService : IPessoaService
    {
        private volatile int contador;

        public Pessoa Atualizar(Pessoa pessoa)
        {
            return pessoa;
        }

        public Pessoa Criar(Pessoa pessoa)
        {
            return pessoa;
        }

        public void Excluir(long id)
        {
        }

        public Pessoa RetornarPorId(long id)
        {
            return new Pessoa
            {
                Id = GerarId(),
                Nome = "Lucas",
                Sobrenome = "Alcântara",
                Endereco = "Contagem/MG",
                Genero = "Masculino"
            };
        }

        public List<Pessoa> RetornarTodos()
        {
            var pessoas = new List<Pessoa>();

            for (int i = 0; i < 8; i++)
            {
                var pessoa = MockPessoa(i);
                pessoas.Add(pessoa);
            }

            return pessoas;
        }

        private Pessoa MockPessoa(int i)
        {
            return new Pessoa
            {
                Id = GerarId(),
                Nome = $"Nome {i}",
                Sobrenome = $"Sobrenome {i}",
                Endereco = $"Endereço qualquer {i}",
                Genero = i % 2 == 0 ? "Masculino" : "Feminino"
            };
        }

        private long GerarId()
        {
            return Interlocked.Increment(ref contador);
        }
    }
}
