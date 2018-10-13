using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Validacoes
{
    internal class ValidadorCalculadora
    {
        public static bool Numerico(string valor)
        {
            decimal numero;

            // Como o valor numérico pode estar em diversos formatos, precisamos utilizar o NumberStyles.
            bool numerico = decimal.TryParse(valor,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out numero);

            return numerico;
        }
    }
}
