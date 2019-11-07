using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Connect4.Controllers
{
    [Route("api/JogadorMaquina")]
    [ApiController]
    public class JogadorMaquinaAPIController : ControllerBase
    {
        [HttpPost(Name = "RealizarJogada")]
        [Route("RealizarJogada")]
        public int RealizarJogada(Tabuleiro tabuleiro)
        {
            IJogadorComputador computador = new JogadorMaquinaFacil();
            int result = computador.RealizarJogada(tabuleiro);

            return result;
        }
    }
}