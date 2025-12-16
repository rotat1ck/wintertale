using AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
