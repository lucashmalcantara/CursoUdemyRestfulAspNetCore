using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Services
{
    public interface IArquivoService
    {
        byte[] RetornarArquivPdf();
    }
}
