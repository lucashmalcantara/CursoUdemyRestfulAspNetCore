using RestfulAspNetCore.Properties;
using System.Linq;

namespace RestfulAspNetCore.Services.Implementacoes
{
    public class ArquivoService : IArquivoService
    {
        public byte[] RetornarArquivPdf()
        {
            return Resources.Lucas.ToArray();
        }
    }
}
