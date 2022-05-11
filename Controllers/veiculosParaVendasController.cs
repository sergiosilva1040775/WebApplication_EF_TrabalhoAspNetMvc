using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using final.Context;
using final.Models;

namespace final.Controllers
{
    public class veiculosParaVendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public veiculosParaVendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: veiculosParaVendas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.veiculosParaVenda.Include(v => v.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: veiculosParaVendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.veiculosParaVenda == null)
            {
                return NotFound();
            }

            var veiculosParaVenda = await _context.veiculosParaVenda
                .Include(v => v.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculosParaVenda == null)
            {
                return NotFound();
            }

            return View(veiculosParaVenda);
        }

        // GET: veiculosParaVendas/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaName");
            return View();
        }

        // POST: veiculosParaVendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeMarca,NomeModelo,Ano,Cilindrada,Preco,CategoriaId")] veiculosParaVenda veiculosParaVenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veiculosParaVenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaName", veiculosParaVenda.CategoriaId);
            return View(veiculosParaVenda);
        }

        // GET: veiculosParaVendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.veiculosParaVenda == null)
            {
                return NotFound();
            }

            var veiculosParaVenda = await _context.veiculosParaVenda.FindAsync(id);
            if (veiculosParaVenda == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaName", veiculosParaVenda.CategoriaId);
            return View(veiculosParaVenda);
        }

        // POST: veiculosParaVendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeMarca,NomeModelo,Ano,Cilindrada,Preco,CategoriaId")] veiculosParaVenda veiculosParaVenda)
        {
            if (id != veiculosParaVenda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculosParaVenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!veiculosParaVendaExists(veiculosParaVenda.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaName", veiculosParaVenda.CategoriaId);
            return View(veiculosParaVenda);
        }

        // GET: veiculosParaVendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.veiculosParaVenda == null)
            {
                return NotFound();
            }

            var veiculosParaVenda = await _context.veiculosParaVenda
                .Include(v => v.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculosParaVenda == null)
            {
                return NotFound();
            }

            return View(veiculosParaVenda);
        }

        // POST: veiculosParaVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.veiculosParaVenda == null)
            {
                return Problem("Entity set 'ApplicationDbContext.veiculosParaVenda'  is null.");
            }
            var veiculosParaVenda = await _context.veiculosParaVenda.FindAsync(id);
            if (veiculosParaVenda != null)
            {
                _context.veiculosParaVenda.Remove(veiculosParaVenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool veiculosParaVendaExists(int id)
        {
          return (_context.veiculosParaVenda?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
