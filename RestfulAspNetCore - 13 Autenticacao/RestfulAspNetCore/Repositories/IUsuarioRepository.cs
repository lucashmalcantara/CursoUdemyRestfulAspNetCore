using RestfulAspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario RetornarPorAcesso(string acesso);
    }
}
