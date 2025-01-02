using ColApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ColApp.Services;
using ColApp.Authentication;
using ColApp.Interfaces;
using ColApp.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ColApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
            conStrBuilder.Password = builder.Configuration["MDP"];

            // Ajouter la configuration de l'application
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var baseUrl = builder.Configuration["AppSettings:BaseUrl"];  

            builder.Services.AddPooledDbContextFactory<BDEtabContext>(
                x => x.UseSqlServer(conStrBuilder.ConnectionString));

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<ServiceConnexion>();
            builder.Services.AddScoped<ServiceInscription>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>(); // Enregistrement du provider d'authentification
            builder.Services.AddSingleton<UserAccountService>();
           

            // Enregistrer EmailSender dans l'injection de dépendances
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            builder.Services.AddSingleton<ITokenService, TokenService>();
            
            builder.Services.AddSingleton<PasswordResetService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}