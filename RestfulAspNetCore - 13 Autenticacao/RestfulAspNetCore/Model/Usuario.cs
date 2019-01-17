using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Model
{
    public class Usuario
    {
        public long? Id { get; set; }
        public string Acesso { get; set; }
        public string Senha { get; set; }
    }
}
