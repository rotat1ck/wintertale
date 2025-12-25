using Wintertale.Client.Common.DTOs.Requests.Friends;
using Wintertale.Client.Common.DTOs.Responses.Friends;
using Wintertale.Client.Services.BaseApi;

namespace Wintertale.Client.Services.Friends {
    internal class FriendService : IFriendService {
        private readonly IApiService api;

        public FriendService(IApiService api) {
            this.api = api;
        }

        public async Task UpdateUtcOffsetAsync(UpdateUtcOffsetRequest request) {
            await api.HttpAsync(HttpMethod.Post, "api/v1/friends/utc", request);
        }

        public async Task<List<FriendResponse>> GetFriendListAsync() {
            return await api.HttpAsync<List<FriendResponse>>(HttpMethod.Get, "api/v1/friends");
        }

        public async Task<List<FriendResponse>> GetPendingFriendsReceivedAsync() {
            return await api.HttpAsync<List<FriendResponse>>(HttpMethod.Get, "api/v1/friends/pending");
        }

        public async Task<List<FriendResponse>> GetPendingFriendsSentAsync() {
            return await api.HttpAsync<List<FriendResponse>>(HttpMethod.Get, "api/v1/friends/pending/sent");
        }

        public async Task RemoveFriendAsync(RemoveFriendRequest request) {
            await api.HttpAsync(HttpMethod.Delete, "api/v1/friends", request);
        }
    }
}
