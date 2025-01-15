using ColApp.Data;
using ColApp.Services;
using ColApp.Authentication;
using ColApp.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ColApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration de la chaîne de connexion avec le mot de passe
            var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
            conStrBuilder.Password = builder.Configuration["MDP"];

            // Ajouter la configuration de l'application
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Récupérer le BaseUrl depuis appsettings.json (si nécessaire)
            var baseUrl = builder.Configuration["AppSettings:BaseUrl"];

            // Configuration du DbContext
            builder.Services.AddPooledDbContextFactory<BDEtabContext>(
                options => options.UseSqlServer(conStrBuilder.ConnectionString));

            // Ajouter les services Blazor et les services personnalisés
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<ServiceConnexion>();
            builder.Services.AddScoped<ServiceInscription>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddSingleton<UserAccountService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddSingleton<PasswordResetService>();

            // **Ajouter l'authentification**
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie() // Pour les cookies d'authentification
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                googleOptions.CallbackPath = "/signin-google"; // URI de redirection
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
            app.UseAuthentication();
            app.UseAuthorization();

            // Configuration des routes Blazor
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // Lancer l'application
            app.Run();
        }
    }
}
