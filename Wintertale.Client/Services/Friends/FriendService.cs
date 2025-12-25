using Wintertale.Client.Common.DTOs.Requests.Friends;
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
    }
}
