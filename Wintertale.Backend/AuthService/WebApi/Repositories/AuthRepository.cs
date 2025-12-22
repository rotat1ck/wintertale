using AuthService.Application.Interfaces.Repositories;
using Domain.Models;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthService.WebApi.Repositories {
    public class AuthRepository : IAuthRepository {
        private readonly AppDbContext context;

        public AuthRepository(AppDbContext context) {
            this.context = context;
        }

        public async Task<User?> GetUserByIdAsync(string userId) {
            Guid id = Guid.Parse(userId);
            return await context.Users.FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task<User?> GetUserByPhoneAsync(string phone) {
            return await context.Users.FirstOrDefaultAsync(u => u.phone == phone);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken) {
            return await context.RefreshTokens.FirstOrDefaultAsync(t => t.refresh_token == refreshToken);
        }

        public async Task<RefreshToken?> GetRefreshTokenByUserIdAsync(string userId) {
            Guid id = Guid.Parse(userId);
            return await context.RefreshTokens.FirstOrDefaultAsync(t => t.user_id == id);
        }

        public async Task<User> CreateUserAsync(User user) {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken) {
            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<RefreshToken> UpdateRefreshTokenAsync(RefreshToken refreshToken) {
            context.RefreshTokens.Update(refreshToken);
            await context.SaveChangesAsync();
            return refreshToken;
        }
    }
}
