using AutoMapper;
using FriendsService.Application.DTOs.Requests;
using FriendsService.Application.DTOs.Responses;
using FriendsService.Application.Interfaces.Repositories;
using FriendsService.Application.Interfaces.Services;
using FriendsService.Application.Common.Exceptions;

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
            var pendingFriends = await repository.GetPendingFriendsAsync(requesterId);

            var userId = Guid.Parse(requesterId);
            var received = pendingFriends.Where(f => f.user_id_receiver == userId).ToList();

            var sendersIds = received.Select(f => f.user_id_requester).ToList();
            var sendersUsers = await userRepository.GetUsersByIdsAsync(sendersIds);

            var converted = mapper.Map<List<FriendResponse>>(received);
            return mapper.Map(sendersUsers, converted);
        }

        public async Task<List<FriendResponse>> GetPendingFriendsSendedAsync(string requesterId) {
            var pendingFriends = await repository.GetPendingFriendsAsync(requesterId);

            var userId = Guid.Parse(requesterId);
            var sended = pendingFriends.Where(f => f.user_id_requester == userId).ToList();

            var sendersIds = sended.Select(f => f.user_id_requester).ToList();
            var sendersUsers = await userRepository.GetUsersByIdsAsync(sendersIds);

            var converted = mapper.Map<List<FriendResponse>>(sended);
            return mapper.Map(sendersUsers, converted);
        }

        public async Task<FriendResponse> CreateFriendAsync(CreateFriendRequest request, string requesterId) {
            var user = await userRepository.GetUserByPhoneAsync(request.phone)
                ?? throw new UnprocessableException("Пользователь с таким номером телефона не найден");

            var existCheck = await repository.GetFriendByUserAsync();
        }

        public async Task<FriendResponse> UpdateFriendAsync(UpdateFriendRequest request, string requesterId) {

        }

        public async Task RemoveFriendAsync(RemoveFriendRequest request, string requesterId) {

        }
    }
}
