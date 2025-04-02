using Microsoft.AspNetCore.Mvc;
using ControleFinanceiro.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace ControleFinanceiro.Controllers {
    public class DashboardController : Controller {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context) {
            _context = context;
        }

        // Método para exibir o Dashboard
        public IActionResult Index() {
            var totalEntradas = _context.Entradas.Sum(e => e.Valor);
            var totalDespesas = _context.Despesas.Sum(d => d.Valor);
            var SaldoDisponivel = totalEntradas - totalDespesas;



            ViewBag.TotalEntradas = totalEntradas;
            ViewBag.TotalDespesas = totalDespesas;
            ViewBag.SaldoDisponivel = SaldoDisponivel; // Envia o saldo pra view



            return View();
        }

        // Endpoint para retornar os dados em JSON
        [HttpGet]
        public JsonResult GetFinanceData() {
            var totalEntradas = _context.Entradas.Sum(e => e.Valor);
            var totalDespesas = _context.Despesas.Sum(d => d.Valor);

            return Json(new { TotalEntradas = totalEntradas, TotalDespesas = totalDespesas });
        }
        [HttpGet]
        public JsonResult GetChartData() {
            var totalEntradas = _context.Entradas.Sum(e => e.Valor);
            var totalDespesas = _context.Despesas.Sum(d => d.Valor);
            var saldoDisponivel = totalEntradas - totalDespesas;

            // Criar pontos de dados para o gráfico
            var dataPoints = new List<object>
    {
        new { label = "Entradas", y = totalEntradas },
        new { label = "Despesas", y = totalDespesas },
        new { label = "Saldo Disponível", y = saldoDisponivel }
    };

            return Json(dataPoints); // Retorna os dados como JSON
        }
    }
}