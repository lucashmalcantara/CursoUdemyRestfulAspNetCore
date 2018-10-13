using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestfulAspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class CalculadoraController : Controller
    {
        // GET api/values/5/5
        [HttpGet("{primeiroNumero}/{segundoNumero}")]
        public IActionResult Soma(string primeiroNumero, string segundoNumero)
        {
            if (Numerico(primeiroNumero) && Numerico(segundoNumero))
            {
                var soma = ConverterParaDecimal(primeiroNumero) + ConverterParaDecimal(segundoNumero);
                return Ok(soma.ToString());
            }

            return BadRequest("Entrada inválida.");
        }

        private decimal ConverterParaDecimal(string numero)
        {
            decimal valorDecimal;

            if (decimal.TryParse(numero, out valorDecimal))
                return valorDecimal;

            return 0;
        }

        private bool Numerico(string valor)
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
