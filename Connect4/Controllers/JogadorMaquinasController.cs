using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Connect4.Data;
using Connect4.Models;

namespace Connect4.Controllers
{
    public class JogadorMaquinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JogadorMaquinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JogadorMaquinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.JogadorMaquina.ToListAsync());
        }

        // GET: JogadorMaquinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorMaquina = await _context.JogadorMaquina
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogadorMaquina == null)
            {
                return NotFound();
            }

            return View(jogadorMaquina);
        }

        // GET: JogadorMaquinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JogadorMaquinas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("URLServico,NomeMaquina,Id")] JogadorMaquina jogadorMaquina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogadorMaquina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jogadorMaquina);
        }

        // GET: JogadorMaquinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorMaquina = await _context.JogadorMaquina.FindAsync(id);
            if (jogadorMaquina == null)
            {
                return NotFound();
            }
            return View(jogadorMaquina);
        }

        // POST: JogadorMaquinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("URLServico,NomeMaquina,Id")] JogadorMaquina jogadorMaquina)
        {
            if (id != jogadorMaquina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogadorMaquina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorMaquinaExists(jogadorMaquina.Id))
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
            return View(jogadorMaquina);
        }

        // GET: JogadorMaquinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorMaquina = await _context.JogadorMaquina
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogadorMaquina == null)
            {
                return NotFound();
            }

            return View(jogadorMaquina);
        }

        // POST: JogadorMaquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogadorMaquina = await _context.JogadorMaquina.FindAsync(id);
            _context.JogadorMaquina.Remove(jogadorMaquina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogadorMaquinaExists(int id)
        {
            return _context.JogadorMaquina.Any(e => e.Id == id);
        }
    }
}
