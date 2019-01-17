using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Services
{
    public interface ILoginService
    {
        object RetornarPorAcesso(UsuarioVo usuario);
    }
}
