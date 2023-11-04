using Data_Access;
using Data_Access.IRepositorios;
using Data_Access.Repositorios;
using Microsoft.EntityFrameworkCore;
using Servicios.IServicios;
using Servicios.Servicios;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Añado que el builder de Session
            builder.Services.AddSession();

            builder.Services.AddDbContext<DbContext, MiContexto>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
            });

            // Scopes Servicios

            
            builder.Services.AddScoped(typeof(IServicioAudit), typeof(ServicioAudit));
            builder.Services.AddScoped(typeof(IServicioAmenaza), typeof(ServicioAmenaza));
            builder.Services.AddScoped(typeof(IServicioEspecie), typeof(ServicioEspecie));
            builder.Services.AddScoped(typeof(IServicioEstadoConservacion), typeof(ServicioEstadoConservacion));
            builder.Services.AddScoped(typeof(IServicioPais), typeof(ServicioPais));  
            builder.Services.AddScoped(typeof(IServicioEcosistemaMarino), typeof(ServicioEcosistemaMarino));
            builder.Services.AddScoped(typeof(IServicioEcosistemaMarinoEspecie), typeof(ServicioEcosistemaMarinoEspecie));
            builder.Services.AddScoped(typeof(IServicioEcosistemaAmenaza), typeof(ServicioEcosistemaAmenaza));
            builder.Services.AddScoped(typeof(IServicioUsuario), typeof(ServicioUsuario));
            builder.Services.AddScoped(typeof(IServicioEspecieAmenaza), typeof(ServicioEspecieAmenaza));

            
            builder.Services.AddScoped(typeof(IRepositorioAudit), typeof(RepositorioAudit));
            builder.Services.AddScoped(typeof(IRepositorioAmenaza), typeof(RepositorioAmenaza));
            builder.Services.AddScoped(typeof(IRepositorioEspecieAmenaza), typeof(RepositorioEspecieAmenaza));
            builder.Services.AddScoped(typeof(IRepositorioEspecie), typeof(RepositorioEspecie));
            builder.Services.AddScoped(typeof(IRepositorioEstadoConservacion), typeof(RepositorioEstadoConservacion));
            builder.Services.AddScoped(typeof(IRepositorioPais), typeof(RepositorioPais));
            builder.Services.AddScoped(typeof(IRepositorioEcosistemaMarino), typeof(RepositorioEcosistemaMarino));
            builder.Services.AddScoped(typeof(IRepositorioEcosistemaAmenaza), typeof(RepositorioEcosistemaAmenaza));
            builder.Services.AddScoped(typeof(IRepositorioEcosistemaMarinoEspecie), typeof(RepositorioEcosistemaMarinoEspecie));
            builder.Services.AddScoped(typeof(IRepositorioUsuario), typeof(RepositorioUsuario));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Añado que la app use Session
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Usuario}/{action=LogIn}/{id?}");

            app.Run();
        }
    }
}