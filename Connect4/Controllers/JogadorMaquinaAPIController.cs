using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Connect4.Controllers
{
    //[Produces("application/json")]
    [Route("api/JogadorMaquina")]
    [ApiController]
    public class JogadorMaquinaAPIController : ControllerBase
    {
        [HttpGet(Name = "teste")]
        [Route("teste")]
        public int teste()
        {
            return 30;
        }

        [HttpPost(Name = "RealizarJogada")]
        [Route("RealizarJogada")]
        public int RealizarJogada(Tabuleiro tabuleiro)
        {
            IJogadorComputador computador = new JogadorMaquinaFacil();
            return computador.RealizarJogada(tabuleiro);
        }
    }
}