using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

public class EntradasController : Controller {
    private readonly AppDbContext _context;

    public EntradasController(AppDbContext context) {
        _context = context;
    }

    // Listar todas as entradas
    public IActionResult Index() {
        var entradas = _context.Entradas.ToList();
        return View(entradas);
    }

    // Formulário para criar nova entrada
    public IActionResult Create() {
        return View();
    }

    // Salvar uma nova entrada
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Entrada entrada) {
        if (ModelState.IsValid)
        {
            _context.Entradas.Add(entrada);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(entrada);
    }

    // Método para confirmar exclusão (GET)
    public IActionResult Delete(int? id) {
        if (id == null)
        {
            return NotFound();
        }

        var entrada = _context.Entradas.FirstOrDefault(e => e.Id == id);
        if (entrada == null)
        {
            return NotFound();
        }

        return View(entrada); // Retorna uma View para confirmar a exclusão
    }

    // Método para excluir a entrada (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var entrada = _context.Entradas.Find(id);
        if (entrada != null)
        {
            _context.Entradas.Remove(entrada);
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Index)); // Redireciona para a página de listagem
    }
}