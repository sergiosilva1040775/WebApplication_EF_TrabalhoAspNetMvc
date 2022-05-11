using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using final.Context;
using final.Models;

namespace final
{
    public class LicitacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LicitacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Licitacoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Licitacoes.Include(l => l.veiculosParaVenda);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Licitacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Licitacoes == null)
            {
                return NotFound();
            }

            var licitacoes = await _context.Licitacoes
                .Include(l => l.veiculosParaVenda)
                .FirstOrDefaultAsync(m => m.LicitacoesId == id);
            if (licitacoes == null)
            {
                return NotFound();
            }

            return View(licitacoes);
        }

        // GET: Licitacoes/Create
        public IActionResult Create()
        {
            ViewData["veiculosParaVendaId"] = new SelectList(_context.Set<veiculosParaVenda>(), "Id", "Ano");
            return View();
        }

        // POST: Licitacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LicitacoesId,licitador,valorLicitado,veiculosParaVendaId")] Licitacoes licitacoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(licitacoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["veiculosParaVendaId"] = new SelectList(_context.Set<veiculosParaVenda>(), "Id", "Ano", licitacoes.veiculosParaVendaId);
            return View(licitacoes);
        }

        // GET: Licitacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Licitacoes == null)
            {
                return NotFound();
            }

            var licitacoes = await _context.Licitacoes.FindAsync(id);
            if (licitacoes == null)
            {
                return NotFound();
            }
            ViewData["veiculosParaVendaId"] = new SelectList(_context.Set<veiculosParaVenda>(), "Id", "Ano", licitacoes.veiculosParaVendaId);
            return View(licitacoes);
        }

        // POST: Licitacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LicitacoesId,licitador,valorLicitado,veiculosParaVendaId")] Licitacoes licitacoes)
        {
            if (id != licitacoes.LicitacoesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(licitacoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicitacoesExists(licitacoes.LicitacoesId))
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
            ViewData["veiculosParaVendaId"] = new SelectList(_context.Set<veiculosParaVenda>(), "Id", "Ano", licitacoes.veiculosParaVendaId);
            return View(licitacoes);
        }

        // GET: Licitacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Licitacoes == null)
            {
                return NotFound();
            }

            var licitacoes = await _context.Licitacoes
                .Include(l => l.veiculosParaVenda)
                .FirstOrDefaultAsync(m => m.LicitacoesId == id);
            if (licitacoes == null)
            {
                return NotFound();
            }

            return View(licitacoes);
        }

        // POST: Licitacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Licitacoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Licitacoes'  is null.");
            }
            var licitacoes = await _context.Licitacoes.FindAsync(id);
            if (licitacoes != null)
            {
                _context.Licitacoes.Remove(licitacoes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LicitacoesExists(int id)
        {
          return (_context.Licitacoes?.Any(e => e.LicitacoesId == id)).GetValueOrDefault();
        }
    }
}
