using AutoMapper;
using FriendsService.Application.DTOs.Responses;
using FriendsService.Application.Interfaces.Repositories;
using FriendsService.Application.Interfaces.Services;
using FriendsService.Domain.Models;

namespace FriendsService.WebApi.Services {
    public class FriendService : IFriendService {
        private readonly IFriendRepository repository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public FriendService(IFriendRepository repository, IUserRepository userRepository, IMapper mapper) {
            this.repository = repository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<List<FriendResponse>> GetFriendListAsync(string requesterId) {
            var acceptedFriends = await repository.GetAcceptedFriendsAsync(requesterId);

            Guid userId = Guid.Parse(requesterId);
            var friendsIds = acceptedFriends
                .Select(f => f.user_id_requester == userId ? f.user_id_receiver : f.user_id_requester)
                .ToList();

            var friendsUsers = await userRepository.GetUsersByIdsAsync(friendsIds);

            var converted = mapper.Map<List<FriendResponse>>(acceptedFriends);
            return mapper.Map(friendsUsers, converted);
        }

        public async Task<List<FriendResponse>> GetPendingFriendsReceivedAsync(string requesterId) {

        }

        public async Task<List<FriendResponse>> GetPendingFriendsSendedAsync(string requesterId) {

        }
    }
}
