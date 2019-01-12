using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace RestfulAspNetCore.Data.ValueObjects
{
    // O padrão VO auxilia na segurança da aplicação.Evita expor a classe que representa nosso banco.Cria-se uma nova classe responsável para a exposição dos dados.
    public class PessoaVo : ISupportsHyperMedia
    {
        public long? Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Endereco { get; set; }
        public char Genero { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
