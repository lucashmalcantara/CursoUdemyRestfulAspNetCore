using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Data.ValueObjects
{
    public class UsuarioVo
    {
        public long? Id { get; set; }
        public string Acesso { get; set; }
        public string Senha { get; set; }
    }
}
