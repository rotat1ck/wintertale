using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Friend> Friends { get; set; }
    }
}
