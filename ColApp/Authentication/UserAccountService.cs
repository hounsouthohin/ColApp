using Microsoft.EntityFrameworkCore;
using ColApp.Data;
using ColApp.Models;

namespace ColApp.Services
{

    public class UserAccountService
    {
        public readonly IDbContextFactory<BDEtabContext> _factory;

        public UserAccountService(IDbContextFactory<BDEtabContext> factory)
        {
            _factory = factory;
        }
        public Utilisateur? GetByUserMail(string courriel)
        {
            var dbContext = _factory.CreateDbContext();
            var user = dbContext.Utilisateurs
                        .Where(x => x.Courriel == courriel)
                        .FirstOrDefault();
            return user;

        }
    }
}
