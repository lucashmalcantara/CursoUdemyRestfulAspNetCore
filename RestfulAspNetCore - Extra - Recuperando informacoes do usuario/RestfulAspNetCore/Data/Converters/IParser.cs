using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Data.Converters
{
    public interface IParser<O, D>
    {
        D Trasformar(O origem);
        List<D> Transformar(List<O> origem);
    }
}
