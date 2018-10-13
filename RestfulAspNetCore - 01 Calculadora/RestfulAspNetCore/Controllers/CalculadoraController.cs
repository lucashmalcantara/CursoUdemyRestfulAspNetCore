using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Util;
using RestfulAspNetCore.Validacoes;

namespace RestfulAspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class CalculadoraController : Controller
    {
        // GET api/Calculadora/Soma/5/5
        [HttpGet("soma/{primeiroNumero}/{segundoNumero}")]
        public IActionResult Soma(string primeiroNumero, string segundoNumero)
        {
            if (ValidadorCalculadora.Numerico(primeiroNumero) && ValidadorCalculadora.Numerico(segundoNumero))
            {
                var soma = Conversor.ParaDecimal(primeiroNumero) + Conversor.ParaDecimal(segundoNumero);
                return Ok(soma.ToString());
            }

            return BadRequest("Entrada inválida.");
        }

        // GET api/Calculadora/Subtracao/5/5
        [HttpGet("subtracao/{primeiroNumero}/{segundoNumero}")]
        public IActionResult Subtracao(string primeiroNumero, string segundoNumero)
        {
            if (ValidadorCalculadora.Numerico(primeiroNumero) && ValidadorCalculadora.Numerico(segundoNumero))
            {
                var subtracao = Conversor.ParaDecimal(primeiroNumero) - Conversor.ParaDecimal(segundoNumero);
                return Ok(subtracao.ToString());
            }

            return BadRequest("Entrada inválida.");
        }

        // GET api/Calculadora/Multiplicacao/5/5
        [HttpGet("multiplicacao/{primeiroNumero}/{segundoNumero}")]
        public IActionResult Multiplicacao(string primeiroNumero, string segundoNumero)
        {
            if (ValidadorCalculadora.Numerico(primeiroNumero) && ValidadorCalculadora.Numerico(segundoNumero))
            {
                var multiplicacao = Conversor.ParaDecimal(primeiroNumero) * Conversor.ParaDecimal(segundoNumero);
                return Ok(multiplicacao.ToString());
            }

            return BadRequest("Entrada inválida.");
        }

        // GET api/Calculadora/Divisao/5/5
        [HttpGet("divisao/{primeiroNumero}/{segundoNumero}")]
        public IActionResult Divisao(string primeiroNumero, string segundoNumero)
        {
            if (ValidadorCalculadora.Numerico(primeiroNumero) && ValidadorCalculadora.Numerico(segundoNumero))
            {
                var divisao = Conversor.ParaDecimal(primeiroNumero) / Conversor.ParaDecimal(segundoNumero);
                return Ok(divisao.ToString());
            }

            return BadRequest("Entrada inválida.");
        }

        // GET api/Calculadora/Media/5/5
        [HttpGet("media/{primeiroNumero}/{segundoNumero}")]
        public IActionResult Media(string primeiroNumero, string segundoNumero)
        {
            if (ValidadorCalculadora.Numerico(primeiroNumero) && ValidadorCalculadora.Numerico(segundoNumero))
            {
                var media = (Conversor.ParaDecimal(primeiroNumero) / Conversor.ParaDecimal(segundoNumero)) / 2;
                return Ok(media.ToString());
            }

            return BadRequest("Entrada inválida.");
        }

        // GET api/Calculadora/RaizQuadrada/5/5
        [HttpGet("raizquadrada/{numero}")]
        public IActionResult RaizQuadrada(string numero)
        {
            if (ValidadorCalculadora.Numerico(numero))
            {
                var raizQuadrada = Math.Sqrt((double)Conversor.ParaDecimal(numero));
                return Ok(raizQuadrada.ToString());
            }

            return BadRequest("Entrada inválida.");
        }
    }
}
