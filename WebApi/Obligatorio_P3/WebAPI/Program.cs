
using Data_Access;
using Data_Access.IRepositorios;
using Data_Access.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Servicios.IServicios;
using Servicios.Servicios;
using System.Text;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll",
                    builder => {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            // Add services to the container.

            //builder.Services.AddSession();
            //builder.Services.AddDistributedMemoryCache();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<DbContext, MiContexto>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
            });
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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("unaClaveSeguraCienPorCientoRealNoFake"))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("AllowAll");
            }
            

            //app.UseSession();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}