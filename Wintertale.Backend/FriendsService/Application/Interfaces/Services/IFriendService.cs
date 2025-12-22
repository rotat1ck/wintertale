using FriendsService.Application.DTOs.Requests;
using FriendsService.Application.DTOs.Responses;

namespace FriendsService.Application.Interfaces.Services {
    public interface IFriendService {
        Task<List<FriendResponse>> GetAcceptedFriendsAsync(string requesterId);
        Task<List<FriendResponse>> GetPendingFriendsAsync(string requesterId);

        Task<FriendResponse> CreateFriendAsync(CreateFriendRequest request, string requesterId);
        Task<FriendResponse> UpdateFriendAsync(UpdateFriendRequest request, string requesterId);
        Task RemoveFriendAsync(RemoveFriendRequest request, string requesterId);
    }
}
