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
            return View(await _context.Torneio.ToListAsync());
        }

        // GET: Torneios/Details/5
        public async Task<IActionResult> Details(int? id)
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
    }
}
