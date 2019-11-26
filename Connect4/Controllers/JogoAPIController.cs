using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Connect4.Controllers
{
    [Route("api/Jogo")]
    [ApiController]
    [Authorize]
    public class JogoAPIController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private ApplicationDbContext _context { get; set; }

        public JogoAPIController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ApplicationDbContext dbcontext)
        {
            this._userManager = userManager;
            this._context = dbcontext;
        }

        [HttpGet(Name = "Obter")]
        [Route("Obter")]
        public Tabuleiro Obter()
        {
            return new Tabuleiro();
        }

        [HttpGet(Name = "Obter")]
        [Route("Obter/{id}")]
        [Authorize]
        public IActionResult ObterJogo(int id)
        {
            var jogo = _context.Jogo.Include(j => j.tabuleiro).Where(j => j.Id == id).FirstOrDefault();

            if (jogo == null)
            {
                throw new ApplicationException("O jogo não existe");
            }

            int? jogadorId = _userManager.GetUserAsync(User).Result.JogadorId;
            if (!(jogadorId == jogo.Jogador1Id || jogadorId == jogo.Jogador2Id))
            {
                return Forbid();
            }

            if (jogo.tabuleiro != null)
            {
                return Ok(jogo.tabuleiro);
            }
            jogo.tabuleiro = new Tabuleiro();
            _context.SaveChanges();
            return Ok(jogo.tabuleiro);
        }

        [HttpGet(Name = "VerificaVencedor")]
        [Route("VerificaVencedor/{id}")]
        public IActionResult VerificaVencedor(int id)
        {
            var jogo = _context.Jogo.Include(j => j.tabuleiro).Include(j => j.Jogador1).Include(j => j.Jogador2).Where(j => j.Id == id).FirstOrDefault();

            if (jogo == null)
            {
                return NotFound();
            }

            if (jogo.tabuleiro == null)
            {
                return BadRequest();
            }

            if (jogo.Jogador1 is JogadorPessoa)
            {
                JogadorPessoa jp = new JogadorPessoa();
                jp = (JogadorPessoa)jogo.Jogador1;
                jp.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == jogo.Jogador1Id).FirstOrDefault();
                jogo.Jogador1 = jp;
            }

            if (jogo.Jogador2 is JogadorPessoa)
            {
                JogadorPessoa jp = new JogadorPessoa();
                jp = (JogadorPessoa)jogo.Jogador2;
                jp.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == jogo.Jogador2Id).FirstOrDefault();
                jogo.Jogador2 = jp;
            }

            int? jogadorId = _userManager.GetUserAsync(User).Result.JogadorId;
            if (!(jogadorId == jogo.Jogador1Id || jogadorId == jogo.Jogador2Id))
            {
                return Forbid();
            }

            jogo.tabuleiro.VerificaVencedor();
            _context.SaveChanges();

            if (jogo.tabuleiro.Vencedor == 1)
            {
                return Ok(new { message = "Parabéns, o jogador 1 (" + jogo.Jogador1.Nome + ") venceu o jogo." });
            }
            else if (jogo.tabuleiro.Vencedor == 2)
            {
                return Ok(new { message = "Parabéns, o jogador 2 (" + jogo.Jogador2.Nome + ") venceu o jogo." });
            }
            else if (jogo.tabuleiro.Vencedor == -1)
            {
                return Ok(new { message = "Ops, o jogo terminou empatado." });
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpGet(Name = "Jogar")]
        [Route("Jogar")]
        public IActionResult Jogar(int JogoId, int coluna)
        {
            var jogo = _context.Jogo.Include(j => j.tabuleiro).Include(j => j.Jogador1).Include(j => j.Jogador2).Where(j => j.Id == JogoId).FirstOrDefault();

            if (jogo == null)
            {
                return NotFound();
            }

            if(jogo.tabuleiro == null)
            {
                return BadRequest();
            }

            if (jogo.Jogador1 is JogadorPessoa)
            {
                JogadorPessoa jp = new JogadorPessoa();
                jp = (JogadorPessoa)jogo.Jogador1;
                jp.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == jogo.Jogador1Id).FirstOrDefault();
                jogo.Jogador1 = jp;
            }

            if (jogo.Jogador2 is JogadorPessoa)
            {
                JogadorPessoa jp = new JogadorPessoa();
                jp = (JogadorPessoa)jogo.Jogador2;
                jp.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == jogo.Jogador2Id).FirstOrDefault();
                jogo.Jogador2 = jp;
            }

            if (jogo.tabuleiro.Vencedor == 1)
            {
                return BadRequest(new Exception("O jogo já foi vencido pelo jogador 1 (" + jogo.Jogador1.Nome + ")."));
            }else if (jogo.tabuleiro.Vencedor == 2)
            {
                return BadRequest(new Exception("O jogo já foi vencido pelo jogador 2 (" + jogo.Jogador2.Nome + ")."));
            }else if(jogo.tabuleiro.Vencedor == -1)
            {
                return BadRequest(new Exception("O jogo já terminou empatado."));
            }

            int? jogadorId = _userManager.GetUserAsync(User).Result.JogadorId;
            if (!(jogadorId == jogo.Jogador1Id || jogadorId == jogo.Jogador2Id))
            {
                return Forbid();
            }

            int? currentPlayerId;
            if (jogo.tabuleiro.JogadorAtual == 1)
            {
                currentPlayerId = jogo.Jogador1Id;
            }
            else
            {
                currentPlayerId = jogo.Jogador2Id;
            }

            if(jogadorId != currentPlayerId)
            {
                return BadRequest(new Exception("Não é a sua vez de jogar"));
            }

            try
            {
                jogo.tabuleiro.Jogar(coluna, jogo.tabuleiro.JogadorAtual);
                _context.SaveChanges();
            }catch(Exception e)
            {
                return BadRequest(e);
            }

            return Ok(jogo.tabuleiro);
        }
    }
}