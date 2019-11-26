using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Connect4.Controllers
{
    public class JogoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public JogoController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              ApplicationDbContext dbContext)
        {
            this._context = dbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        /// <summary>
        /// Ação para criar o jogo.
        /// Verifica se já existe um jogo faltando jogador, se existir utiliza esse, se não cria um novo.
        /// </summary>
        /// <returns>Redireciona o usuário para o jogo.</returns>
        public IActionResult CriarJogo()
        {
            Jogo Jogo = _context.Jogo.Include(j => j.Jogador1)
                                     .Include(j => j.Jogador2)
                                     .FirstOrDefault(j => j.Jogador1 == null || j.Jogador2 == null);
            var jogadorData = _userManager.GetUserAsync(User).Result;
            int? jogadorId = jogadorData.JogadorId;
            JogadorPessoa player = _context.JogadorPessoa.Find(jogadorId);
            player.Usuario = jogadorData;

            if(player == null || player.Id == 0)
            {
                return NotFound();
            }

            if (Jogo == null)
            {
                Jogo = new Jogo
                {
                    Jogador1 = player,
                    tabuleiro = new Tabuleiro()
                };
                _context.Add(Jogo);
            }
            else if (Jogo.Jogador1 != player && Jogo.Jogador2 != player)
            {
                if(Jogo.Jogador1 == null)
                {
                    Jogo.Jogador1 = player;
                }
                else if (Jogo.Jogador2 == null)
                {
                    Jogo.Jogador2 = player;
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Lobby), new { id = Jogo.Id });
        }
        
        [Authorize]
        public IActionResult ContinuarJogo()
        {
            var jogadorData = _userManager.GetUserAsync(User).Result;
            var jogos = _context.Jogo.Include(j => j.Jogador1)
                                     .Include(j => j.Jogador2)
                                     .Include(j => j.tabuleiro)
                                     .Where(j => (j.Jogador1.Id == jogadorData.JogadorId 
                                                    || j.Jogador2.Id == jogadorData.JogadorId));

            foreach(Jogo jogo in jogos)
            {
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

                if(jogo.tabuleiro == null)
                {
                    jogo.tabuleiro = new Tabuleiro();
                }
            }

            return View(jogos);
        }

        [Authorize]
        public IActionResult Lobby(int id)
        {
            Jogo jogo = _context.Jogo.Include(j => j.Jogador1)
                                     .Include(j => j.Jogador2)
                                     .Include(j => j.tabuleiro)
                                     .Where(j => j.Id == id).Select(j => j).FirstOrDefault();
            if(jogo == null)
            {
                return NotFound();
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

            return View(jogo);
        }

        [Authorize]
        public IActionResult Tabuleiro(int id)
        {
            Jogo jogo = _context.Jogo.Include(j => j.Jogador1).Include(j => j.Jogador2).Include(j => j.tabuleiro).Where(j => j.Id == id).Select(j => j).FirstOrDefault();
            if (jogo == null)
            {
                return NotFound();
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

            if (jogo.tabuleiro == null)
            {
                jogo.tabuleiro = new Tabuleiro();
                _context.SaveChanges();
            }

            ViewData["authPlayerId"] = _userManager.GetUserAsync(User).Result.JogadorId;

            return View(jogo);
        }
    }
}