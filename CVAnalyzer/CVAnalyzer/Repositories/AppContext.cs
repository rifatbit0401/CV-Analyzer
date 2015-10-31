using CVAnalyzer.Authentication.Model;
using CVAnalyzer.Models;
using System.Data.Entity;

namespace CVAnalyzer.Repositories
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; } 
    }
}