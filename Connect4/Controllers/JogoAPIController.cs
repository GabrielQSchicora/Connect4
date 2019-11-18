using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Connect4.Controllers
{
    [Route("api/Jogo")]
    [ApiController]
    [Authorize]
    public class JogoAPIController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private ApplicationDbContext _context { get; set; }

        public JogoAPIController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ApplicationDbContext dbcontext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = dbcontext;
        }

        [HttpGet(Name = "Obter")]
        [Route("Obter")]
        public Tabuleiro Obter()
        {
            var user = _userManager.GetUserAsync(this.User).Result;
            return new Tabuleiro();
        }

        [HttpGet(Name = "Obter")]
        [Route("Obter/{id}")]
        [Authorize]
        public Tabuleiro ObterJogo(int id)
        {
            return _context.Tabuleiros.Find(id);
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