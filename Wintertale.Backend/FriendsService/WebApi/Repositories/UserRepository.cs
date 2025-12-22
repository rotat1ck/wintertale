using FriendsService.Application.Interfaces.Repositories;
using FriendsService.Domain.Models;
using FriendsService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FriendsService.WebApi.Repositories {
    public class UserRepository : IUserRepository {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context) {
            this.context = context;
        }

        public async Task<User?> GetUserByIdAsync(string id) {
            Guid userId = Guid.Parse(id);
            return await context.Users.FirstOrDefaultAsync(u => u.id == userId);
        }

        public async Task<List<User>> GetUsersByIdsAsync(List<Guid> ids) {
            return await context.Users.Where(u => ids.Contains(u.id)).ToListAsync();    
        }

        public async Task<User?> GetUserByPhoneAsync(string phone) {
            return await context.Users.FirstOrDefaultAsync(u => u.phone == phone);
        }
    }
}
