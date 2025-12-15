using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) { }

        
    }
}
