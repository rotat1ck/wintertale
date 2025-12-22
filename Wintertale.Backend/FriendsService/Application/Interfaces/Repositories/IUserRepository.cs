using FriendsService.Domain.Models;

namespace FriendsService.Application.Interfaces.Repositories {
    public interface IUserRepository {
        Task<User?> GetUserByPhoneAsync(string phone);
    }
}
