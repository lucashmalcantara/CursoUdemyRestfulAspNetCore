using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Data.ValueObjects
{
    [DataContract]
    public class LivroVo
    {
        [DataMember(Order = 1, Name = "codigo")]
        public long? Id { get; set; }

        [DataMember(Order = 2)]
        public string Titulo { get; set; }

        [DataMember(Order = 3)]
        public string Autor { get; set; }

        [DataMember(Order = 5)]
        public decimal Preco { get; set; }

        [DataMember(Order = 4)]
        public DateTime DataLancamento { get; set; }
    }
}
