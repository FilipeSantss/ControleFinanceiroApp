using System;
using System.ComponentModel.DataAnnotations;
using ControleFinanceiro.Data;
using Microsoft.EntityFrameworkCore;


namespace ControleFinanceiroRebuild {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Configure o DbContext com MySQL (antes de builder.Build)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 23)) // Altere conforme a versão do seu MySQL
                ));

            // Adiciona serviços ao container
            builder.Services.AddControllersWithViews();

            // Constrói o aplicativo
            var app = builder.Build();

            // Configura o pipeline de requisições HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // O valor padrão para HSTS é 30 dias. Altere para produção, se necessário.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}