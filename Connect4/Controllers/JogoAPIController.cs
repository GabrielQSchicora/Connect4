using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Connect4.Controllers
{
    [Route("api/Jogo")]
    [ApiController]
    public class JogoAPIController : ControllerBase
    {
        [HttpGet(Name = "Obter")]
        [Route("Obter")]
        public Tabuleiro Obter()
        {
            return new Tabuleiro();
        }

        [HttpPost(Name = "VerificaVencedor")]
        [Route("VerificaVencedor")]
        public int VerificaVencedor(Tabuleiro tabuleiro)
        {
            return tabuleiro.VerificaVencedor();
        }

        [HttpPost(Name = "Jogar")]
        [Route("Jogar")]
        public Tabuleiro Jogar([FromBody] Tabuleiro tabuleiro, [FromQuery] int coluna, [FromQuery] int jogador)
        {
            tabuleiro.Jogar(coluna, jogador);

            return tabuleiro;
        }
    }
}