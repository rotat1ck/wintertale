using System;
using System.Collections.Generic;
using System.Text;
using Wintertale.Client.Common.DTOs.Requests.Friends;

namespace Wintertale.Client.Services.Friends {
    internal interface IFriendService {
        Task UpdateUtcOffsetAsync(UpdateUtcOffsetRequest request);
    }
}
