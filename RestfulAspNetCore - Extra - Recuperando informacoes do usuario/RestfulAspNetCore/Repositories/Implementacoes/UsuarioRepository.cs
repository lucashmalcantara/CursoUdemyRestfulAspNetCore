using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulAspNetCore.Model;
using RestfulAspNetCore.Model.Context;

namespace RestfulAspNetCore.Repositories.Implementacoes
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MySqlContext _context;

        public UsuarioRepository(MySqlContext context)
        {
            _context = context;
        }

        public Usuario RetornarPorAcesso(string acesso)
        {
            return _context.Usuarios.SingleOrDefault(u => u.Acesso.Equals(acesso));
        }
    }
}
