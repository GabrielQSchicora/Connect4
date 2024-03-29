﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Connect4.Models.ViewModel;
using Connect4.Views.Torneios;

namespace Connect4.Controllers
{
    public class TorneiosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TorneiosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Torneios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Torneio.Include(t => t.Jogadores).Include(t => t.Jogos).ToListAsync());
        }

        // GET: Torneios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneio = await _context.Torneio.Include(t => t.Jogos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (torneio == null)
            {
                return NotFound();
            }

            foreach(Jogo jogo in torneio.Jogos)
            {
                JogadorPessoa jp = new JogadorPessoa();

                jogo.Jogador1 = _context.JogadorPessoa.Where(j => j.Id == jogo.Jogador1Id).FirstOrDefault();
                jp = (JogadorPessoa)jogo.Jogador1;
                jp.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == jogo.Jogador1Id).FirstOrDefault();
                jogo.Jogador1 = jp;

                jogo.Jogador2 = _context.JogadorPessoa.Where(j => j.Id == jogo.Jogador2Id).FirstOrDefault();
                jp = (JogadorPessoa)jogo.Jogador2;
                jp.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == jogo.Jogador2Id).FirstOrDefault();
                jogo.Jogador2 = jp;

                jogo.tabuleiro = _context.Jogo.Include(j => j.tabuleiro).Where(j => j.Id == jogo.Id).Select(j => j.tabuleiro).FirstOrDefault();
            }

            return View(torneio);
        }

        // GET: Torneios/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Torneios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Nome,QuantidadeJogadores,Premiacao,Inicio")] Torneio torneio)
        {
            if (ModelState.IsValid)
            {
                torneio.Dono = User.Identity.Name;
                _context.Add(torneio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(torneio);
        }

        // GET: Torneios/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneio = await _context.Torneio.FindAsync(id);
            if (torneio == null)
            {
                return NotFound();
            }
            return View(torneio);
        }

        // POST: Torneios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,QuantidadeJogadores,Premiacao,Inicio")] Torneio torneio)
        {
            if (id != torneio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var t2 = _context.Torneio.Where(t => t.Id == id).FirstOrDefault();
                    
                    if (t2 == null)
                    {
                        return NotFound();
                    }

                    if (t2.Dono == null)
                    {
                        torneio.Dono = User.Identity.Name;
                    }
                    else
                    {
                        torneio.Dono = t2.Dono;
                    }

                    _context.Entry(t2).State = EntityState.Detached;

                    _context.Update(torneio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TorneioExists(torneio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(torneio);
        }

        // GET: Torneios/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var torneio = await _context.Torneio
                .FirstOrDefaultAsync(m => m.Id == id);

            if (torneio == null)
            {
                return NotFound();
            }

            if (User.Identity.Name != torneio.Dono)
            {
                return Forbid();
            }

            return View(torneio);
        }

        // POST: Torneios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var torneio = await _context.Torneio.FindAsync(id);

            if (User.Identity.Name != torneio.Dono)
            {
                return Forbid();
            }

            _context.Torneio.Remove(torneio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TorneioExists(int id)
        {
            return _context.Torneio.Any(e => e.Id == id);
        }

        public IActionResult SelecionarJogadores(int id)
        {
            var torneio = _context.Torneio.Include(t => t.Jogadores)
                .SingleOrDefault(m => m.Id == id);
            if (torneio == null)
            {
                return NotFound();
            }

            SelecionarUsuarioViewModel viewModel = new SelecionarUsuarioViewModel();

            List<int> jogadores = new List<int>();
            if (torneio.Jogadores != null)
            {
                jogadores = torneio.Jogadores.Select(j => j.Id).ToList();
            }

            List<JogadorPessoa> availableUsersPessoa = _context.JogadorPessoa.Include(j => j.Usuario).ToList();
            List<JogadorMaquina> availableUsersMaquina = _context.JogadorMaquina.ToList();
            List<Jogador> allAvailableUsers = new List<Jogador>();

            foreach (JogadorPessoa item in availableUsersPessoa)
            {
                item.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == item.Id).FirstOrDefault();
                allAvailableUsers.Add((Jogador)item);
            }

            foreach (JogadorMaquina item in availableUsersMaquina)
            {
                allAvailableUsers.Add((Jogador)item);
            }

            ViewBag.Jogadores =
                new SelectList(allAvailableUsers,
                nameof(JogadorPessoa.Id),
                nameof(JogadorPessoa.Nome),
                jogadores
                );
            viewModel.JogadoresIds = jogadores;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SelecionarJogadores(int id, [Bind(nameof(SelecionarUsuarioViewModel.JogadoresIds))] SelecionarUsuarioViewModel viewModel)
        {
            var torneio = _context.Torneio.Include(t => t.Jogadores).SingleOrDefault(m => m.Id == id);
            if (torneio == null)
            {
                return NotFound();
            }

            var jogadores = _context.JogadorPessoa.Where(
                jp => viewModel.JogadoresIds.Contains(jp.Id))
                .ToList();

            torneio.Jogadores.Clear();
            foreach (var item in jogadores)
            {
                torneio.Jogadores.Add(item);
            }
            _context.SaveChanges();

            List<JogadorPessoa> availableUsersPessoa = _context.JogadorPessoa.Include(j => j.Usuario).ToList();
            List<JogadorMaquina> availableUsersMaquina = _context.JogadorMaquina.ToList();
            List<Jogador> allAvailableUsers = new List<Jogador>();

            foreach (JogadorPessoa item in availableUsersPessoa)
            {
                item.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == item.Id).FirstOrDefault();
                allAvailableUsers.Add((Jogador)item);
            }

            foreach (JogadorMaquina item in availableUsersMaquina)
            {
                allAvailableUsers.Add((Jogador)item);
            }

            ViewBag.Jogadores =
                new SelectList(allAvailableUsers,
                nameof(JogadorPessoa.Id),
                nameof(JogadorPessoa.Nome),
                jogadores
                );

            return RedirectToAction(nameof(Index));
        }

        public IActionResult GerarJogos(int id)
        {
            var torneio = _context.Torneio.Include(t => t.Jogadores).SingleOrDefault(m => m.Id == id);
            if (torneio == null)
            {
                return NotFound();
            }

            if(torneio.Jogadores.Count != torneio.QuantidadeJogadores)
            {
                return BadRequest("Quantidade de jogadores insuficiente");
            }

            torneio.GerarJogos();
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public IActionResult Ranking(int id)
        {
            var torneio = _context.Torneio.Include(t => t.Jogadores).Include(t => t.Jogos).SingleOrDefault(m => m.Id == id);
            if (torneio == null)
            {
                return NotFound();
            }

            if(torneio.Jogos == null)
            {
                return BadRequest();
            }

            Dictionary<String,int> ranking = new Dictionary<String, int>();

            foreach (Jogador jogador in torneio.Jogadores)
            {
                if(jogador is JogadorPessoa)
                {
                    JogadorPessoa jp = new JogadorPessoa();
                    jp = (JogadorPessoa)jogador;
                    jp.Usuario = _context.ApplicationUser.Where(j => j.JogadorId == jp.Id).FirstOrDefault();
                    ranking[jp.Nome] = 0;
                }
                else
                {
                    ranking[jogador.Nome] = 0;
                }

            }

            foreach (Jogo jogo in torneio.Jogos)
            {
                Jogo jg = _context.Jogo.Include(j => j.Jogador1).Include(j => j.Jogador2).Include(j => j.tabuleiro).Where(j => j.Id == jogo.Id).FirstOrDefault();

                if(jg.tabuleiro == null)
                {
                    jg.tabuleiro = new Tabuleiro();
                }

                if(jg.tabuleiro.Vencedor == 1)
                {
                    ranking[jg.Jogador1.Nome] = ranking[jg.Jogador1.Nome] + 3;
                }
                else if (jg.tabuleiro.Vencedor == 2)
                {
                    ranking[jg.Jogador2.Nome] = ranking[jg.Jogador2.Nome] + 3;
                }
                else if (jg.tabuleiro.Vencedor == -1)
                {
                    ranking[jg.Jogador1.Nome] = ranking[jg.Jogador1.Nome] + 1;
                    ranking[jg.Jogador2.Nome] = ranking[jg.Jogador2.Nome] + 1;
                }
            }

            var orderRanking = from entry in ranking orderby entry.Value descending select entry;

            RankingModel rm = new RankingModel
            {
                TorneioNome = torneio.Nome,
                ranking = orderRanking.ToDictionary(t => t.Key, t => t.Value)
        };

            return View(rm);
        }
    }
}
