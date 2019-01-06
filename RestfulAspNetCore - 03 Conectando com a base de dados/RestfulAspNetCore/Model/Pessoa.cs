using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Model
{
    // [Table("pessoas")] Isso resolve o problema de case sensitive entre o nome da tabela e a propriedade do C#.
    // Todavia, para evitar problemas, deixei o nome da tabela seguindo as propriedades do C#.
    // https://stackoverflow.com/questions/9445678/entity-framework-with-mysql-table-capitalization-issue-between-linux-and-window
    public class Pessoa
    {
        public long? Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Endereco { get; set; }
        public char Genero { get; set; }
    }
}
