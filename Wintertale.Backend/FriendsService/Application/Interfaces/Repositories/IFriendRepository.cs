using Domain.Models;

namespace FriendsService.Application.Interfaces.Repositories {
    public interface IFriendRepository {
        Task<List<Friend>> GetFriendsAsync(string requesterId);
        Task<List<Friend>> GetAcceptedFriendsAsync(string requesterId);
        Task<List<Friend>> GetPendingFriendsAsync(string requesterId);
        Task<Friend?> GetFriendByUserAsync(User targetUser, string requesterId);

        Task<Friend> CreateFriendAsync(Friend friend);
        Task<Friend> UpdateFriendAsync(Friend friend);
        Task RemoveFriendAsync(Friend friend);
    }
}
