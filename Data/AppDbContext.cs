using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Models;
using System.Collections.Generic;

namespace ControleFinanceiro.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
    }
}
