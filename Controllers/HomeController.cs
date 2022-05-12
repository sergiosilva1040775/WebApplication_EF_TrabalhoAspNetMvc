using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using final.Context;
using final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        

        }
        public async Task<IActionResult> Detalhes(int id)
        {

            var carsLicitacoesModel = new carsLicitacoes();

            veiculosParaVenda cars = new veiculosParaVenda();
            foreach (veiculosParaVenda item in _context.veiculosParaVenda)
            {
                if (item.Id == id)
                {

                    cars = item;

                }

            }
            carsLicitacoesModel.veiculosParaVenda = cars;
            string query = "SELECT * FROM Licitacoes WHERE veiculosParaVendaId = {0}";
            var liticao = await _context.Licitacoes.FromSqlRaw(query, id).ToListAsync();
            carsLicitacoesModel.Licitacoess = liticao;
            return View(carsLicitacoesModel);
         
        }

        public IActionResult Index()
        {
       
            return View(_context.veiculosParaVenda);
        }
        [Authorize]
        public IActionResult licitar(int id)
        {
            ViewData["veiculosParaVendaId"] = new SelectList(_context.Set<veiculosParaVenda>(), "Id", "NomeMarca", id);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> DetailsCategorias(int? id)
        {
            if (id == null || _context.Categoria == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }
            var categoriasCars = new carsCategoria();
            string query = "SELECT * FROM veiculosParaVenda WHERE CategoriaId = {0}";
            var veiculosParaVendalista = await _context.veiculosParaVenda .FromSqlRaw(query, id).ToListAsync();
            categoriasCars.veiculosParaVenda = veiculosParaVendalista;
            categoriasCars.Categorias = categoria;


            return View(categoriasCars);
        }

        public async Task<IActionResult> listaCategorias()
        {
            return _context.Categoria != null ?
                        View(await _context.Categoria.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Categoria'  is null.");
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



      
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
            //ViewData["veiculosParaVendaId"] = new SelectList(_context.Set<veiculosParaVenda>(), "Id", "NomeMarca", licitacoes.veiculosParaVendaId);
            //return View(licitacoes);


            return View(_context.veiculosParaVenda);
        }

    }
}