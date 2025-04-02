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
                    new MySqlServerVersion(new Version(8, 0, 23)) // Altere conforme a vers�o do seu MySQL
                ));

            // Adiciona servi�os ao container
            builder.Services.AddControllersWithViews();

            // Constr�i o aplicativo
            var app = builder.Build();

            // Configura o pipeline de requisi��es HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // O valor padr�o para HSTS � 30 dias. Altere para produ��o, se necess�rio.
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