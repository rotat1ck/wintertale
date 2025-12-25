using System;
using System.Collections.Generic;
using System.Text;
using Wintertale.Client.Common.DTOs.Requests.Friends;
using Wintertale.Client.Common.DTOs.Responses.Friends;

namespace Wintertale.Client.Services.Friends {
    internal interface IFriendService {
        Task UpdateUtcOffsetAsync(UpdateUtcOffsetRequest request);
        Task<List<FriendResponse>> GetFriendListAsync();
        Task<List<FriendResponse>> GetPendingFriendsReceivedAsync();
        Task<List<FriendResponse>> GetPendingFriendsSentAsync();

        Task<FriendResponse> CreateFriendAsync(CreateFriendRequest request);
        Task<FriendResponse> UpdateFriendAsync(UpdateFriendRequest request);
        Task RemoveFriendAsync(RemoveFriendRequest request);
    }
}
