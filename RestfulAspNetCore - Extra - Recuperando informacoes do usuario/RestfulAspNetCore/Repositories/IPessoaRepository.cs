using RestfulAspNetCore.Model;
using RestfulAspNetCore.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Repositories
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        List<Pessoa> RetornarPorNome(string nome, string sobrenome);
    }
}
