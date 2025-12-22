using Domain.Models;

namespace AuthService.Application.Interfaces.Repositories {
    public interface IAuthRepository {
        Task<User?> GetUserByIdAsync(string userId);
        Task<User?> GetUserByPhoneAsync(string phone);
        Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);
        Task<RefreshToken?> GetRefreshTokenByUserIdAsync(string userId);
        Task<User> CreateUserAsync(User user);
        Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> UpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}
