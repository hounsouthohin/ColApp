using ColApp.Authentication;
using ColApp.Data;
using ColApp.Interfaces;
using ColApp.Models;
using ColApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace ColApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Charger les secrets utilisateur si en mode développement     
            /*if (builder.Environment.IsDevelopment())    
            {
                builder.Configuration.AddUserSecrets<Program>();  // Charge les secrets en développement     
            }*/


            // Ajouter la configuration des secrets utilisateur
            builder.Configuration.AddUserSecrets<Program>();

            // Configuration de la chaîne de connexion avec le mot de passe
            var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
            conStrBuilder.Password = builder.Configuration["MDP"];
            
                
            // Ajouter la configuration de l'application
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Configuration du DbContext
            builder.Services.AddPooledDbContextFactory<BDEtabContext>(
                options => options.UseSqlServer(conStrBuilder.ConnectionString));

            // Ajouter les services Blazor et les services personnalisés
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddControllers(); // Prise en charge des contrôleurs
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<ServiceConnexion>();
            builder.Services.AddScoped<ServiceInscription>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>(); // Ajouter explicitement
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddSingleton<UserAccountService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddSingleton<PasswordResetService>();


            // Ajouter l'authentification
            builder.Services.AddAuthentication(options =>
            {   
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options => { 
                  options.Cookie.Name = ".AspNetCore.Cookies"; // Nom du cookie (par défaut)
                
                 
            })
 // Pour les cookies d'authentification
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                googleOptions.CallbackPath = "/controller/signin-google-callback/";// URI de redirection ajouter toujours le / a la fin

                googleOptions.SaveTokens = true;  // Enregistrer les tokens pour éviter les erreurs de session
                                                  // Augmenter le délai pour éviter l'expiration : c a d qu'elle evite des erreurs si google prend plus de temps pour repondre
                googleOptions.BackchannelTimeout = TimeSpan.FromMinutes(2);
                
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1); // Durée de vie de la session
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            // Construire l'application
            var app = builder.Build();

            // Configurer les middlewares
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Ajouter le middleware d'authentification
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            // Configurer les routes des contrôleurs
            app.MapControllers(); // **Ajout des routes des contrôleurs**

            // Configurer les routes Blazor
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // Lancer l'application
            app.Run();
        }
    }
}
