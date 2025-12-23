using FriendsService.Application.Common.Extensions;
using FriendsService.Application.DTOs.Requests;
using FriendsService.Application.DTOs.Responses;
using FriendsService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FriendsService.WebApi.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class FriendsController : Controller {
        private readonly IFriendService service;

        public FriendsController(IFriendService service) {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<FriendResponse>>> GetFriendListAsync() {
            string requesterId = User.GetRequesterId();

            var response = await service.GetFriendListAsync(requesterId);
            return Ok(response);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<List<FriendResponse>>> GetPendingFriendsReceivedAsync() {
            string requesterId = User.GetRequesterId();

            var response = await service.GetPendingFriendsReceivedAsync(requesterId);
            return Ok(response);
        }

        [HttpGet("pending/sent")]
        public async Task<ActionResult<List<FriendResponse>>> GetPendingFriendsSentAsync() {
            string requesterId = User.GetRequesterId();

            var response = await service.GetPendingFriendsSendedAsync(requesterId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<FriendResponse>> CreateFriendRequestAsync(CreateFriendRequest request) {
            string requesterId = User.GetRequesterId();

            var response = await service.CreateFriendAsync(request, requesterId);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<ActionResult<FriendResponse>> UpdateFriendRequestAsync(UpdateFriendRequest request) {
            string requesterId = User.GetRequesterId();

            var response = await service.UpdateFriendAsync(request, requesterId);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveFriendRequestAsync(RemoveFriendRequest request) {
            string requesterId = User.GetRequesterId();

            await service.RemoveFriendAsync(request, requesterId);
            return Ok();
        }

        [HttpPost("utc")]
        public async Task<ActionResult> UpdateUtcOffsetAsync(UpdateUtcOffsetRequest request) {
            string requesterId = User.GetRequesterId();

            await service.UpdateUtcOffsetAsync(request, requesterId);
            return Ok();
        }
    }
}
