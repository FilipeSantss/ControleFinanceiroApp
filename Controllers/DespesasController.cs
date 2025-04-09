﻿using Microsoft.AspNetCore.Mvc;
using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using MySql.Data.MySqlClient;
using System;

namespace ControleFinanceiro.Controllers {
    public class DespesasController : Controller {
        private readonly AppDbContext _context;

        public DespesasController(AppDbContext context) {
            _context = context;
        }

        // Listar todas as despesas
        public IActionResult Index() {
            var despesas = _context.Despesas.ToList(); // Carrega todas as despesas em uma lista
            return View(despesas); // Passa a lista para a View
        }



        // Formulário para criar nova despesa
        public IActionResult Create() {
            return View();
        }

        // Salvar uma nova despesa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Despesa despesa) {
            if (ModelState.IsValid)
            {
                _context.Despesas.Add(despesa);
                _context.SaveChanges();
                return RedirectToAction("Index", "Dashboard"); // Redireciona para o Dashboard
            }
            return View(despesa);

        }
        [HttpPost]
        public IActionResult ExcluirDespesa(int id) {
            string connectionString = "server=caboose.proxy.rlwy.net;port=59800;database=railway;uid=root;pwd=YGCgieczbppztfOUjzUlBcrjYidlhCbE";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM despesas WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                TempData["Mensagem"] = "Despesa excluída com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Ocorreu um erro: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id) {
            // Busca a despesa no banco de dados pelo ID
            var despesa = _context.Despesas.FirstOrDefault(d => d.Id == id);
            if (despesa == null)
            {
                return NotFound(); // Retorna erro 404 se não encontrar a despesa
            }

            return View(despesa); // Retorna a View com os dados da despesa
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Despesa despesa) {
            if (ModelState.IsValid)
            {
                _context.Despesas.Update(despesa); // Atualiza a despesa no banco de dados
                _context.SaveChanges(); // Salva as mudanças
                TempData["Mensagem"] = "Despesa editada com sucesso!";
                return RedirectToAction("Index");
            }

            return View(despesa); // Retorna a View caso algo não esteja válido
        }
    }
}
