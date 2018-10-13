using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Util
{
    internal class Conversor
    {
        public static decimal ParaDecimal(string numero)
        {
            decimal valorDecimal;

            if (decimal.TryParse(numero, out valorDecimal))
                return valorDecimal;

            return 0;
        }
    }
}
