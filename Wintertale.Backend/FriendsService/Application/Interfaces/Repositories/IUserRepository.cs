using Domain.Models;

namespace FriendsService.Application.Interfaces.Repositories {
    public interface IUserRepository {
        Task<User?> GetUserByIdAsync(string id);
        Task<List<User>> GetUsersByIdsAsync(List<Guid> ids);
        Task<User?> GetUserByPhoneAsync(string phone);
    }
}
