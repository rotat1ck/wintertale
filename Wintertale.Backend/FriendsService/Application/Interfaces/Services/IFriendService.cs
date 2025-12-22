using FriendsService.Application.DTOs.Requests;
using FriendsService.Application.DTOs.Responses;

namespace FriendsService.Application.Interfaces.Services {
    public interface IFriendService {
        Task<List<FriendResponse>> GetFriendListAsync(string requesterId);

        /// <summary>
        /// Получить ПОЛУЧЕННЫЕ неподтвержденные запросы
        /// </summary>
        /// <param name="requesterId"></param>
        /// <returns></returns>
        Task<List<FriendResponse>> GetPendingFriendsReceivedAsync(string requesterId);

        /// <summary>
        /// Получить ОТПРАВЛЕННЫЕ неподтвержденные запросы
        /// </summary>
        /// <param name="requesterId"></param>
        /// <returns></returns>
        Task<List<FriendResponse>> GetPendingFriendsSendedAsync(string requesterId);

        Task<FriendResponse> CreateFriendAsync(CreateFriendRequest request, string requesterId);
        Task<FriendResponse> UpdateFriendAsync(UpdateFriendRequest request, string requesterId);
        Task RemoveFriendAsync(RemoveFriendRequest request, string requesterId);
    }
}
