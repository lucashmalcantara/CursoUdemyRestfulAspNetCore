using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RestfulAspNetCore.Model.Base
{
    // Contrato entre os atributos e a estrutura do banco.
    // Também define a ordem em que os atributos aparecerão após a serialização do JSON.
    // [DataContract]
    public class BaseEntity
    {
        //[Key] Define que é a chave da tabela.
        //[Column("Id")] Caso queira especificar o nome da coluna.
        public long? Id { get; set; }
    }
}
