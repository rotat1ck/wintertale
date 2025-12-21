using Microsoft.EntityFrameworkCore;
using FriendsService.Domain.Models;

namespace FriendsService.Persistence.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
