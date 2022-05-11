using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using final.Context;
using final.Models;

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}