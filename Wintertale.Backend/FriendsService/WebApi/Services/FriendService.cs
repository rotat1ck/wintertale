using AutoMapper;
using Domain.Enums;
using Domain.Models;
using FriendsService.Application.Common.Exceptions;
using FriendsService.Application.DTOs.Requests;
using FriendsService.Application.DTOs.Responses;
using FriendsService.Application.Interfaces.Repositories;
using FriendsService.Application.Interfaces.Services;

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

        private List<FriendResponse> CombineFriendResponseData(List<Friend> friends, List<User> users) {
            var friendsData = mapper.Map<List<FriendResponse>>(friends);
            var usersData = mapper.Map<List<FriendResponse>>(users);

            return friendsData.Zip(usersData).Select(values => {
                values.First.fname = values.Second.fname;
                values.First.phone = values.Second.phone;
                values.First.utc_offset = values.Second.utc_offset;
                return values.First;
            }).ToList();
        }

        public async Task<List<FriendResponse>> GetFriendListAsync(string requesterId) {
            var acceptedFriends = await repository.GetAcceptedFriendsAsync(requesterId);

            Guid userId = Guid.Parse(requesterId);
            var friendsIds = acceptedFriends
                .Select(f => f.user_id_requester == userId ? f.user_id_receiver : f.user_id_requester)
                .ToList();

            var friendsUsers = await userRepository.GetUsersByIdsAsync(friendsIds);

            return CombineFriendResponseData(acceptedFriends, friendsUsers);
        }

        public async Task<List<FriendResponse>> GetPendingFriendsReceivedAsync(string requesterId) {
            var pendingFriends = await repository.GetPendingFriendsAsync(requesterId);

            var userId = Guid.Parse(requesterId);
            var received = pendingFriends.Where(f => f.user_id_receiver == userId).ToList();

            var sendersIds = received.Select(f => f.user_id_requester).ToList();
            var sendersUsers = await userRepository.GetUsersByIdsAsync(sendersIds);

            return CombineFriendResponseData(received, sendersUsers);
        }

        public async Task<List<FriendResponse>> GetPendingFriendsSendedAsync(string requesterId) {
            var pendingFriends = await repository.GetPendingFriendsAsync(requesterId);

            var userId = Guid.Parse(requesterId);
            var sended = pendingFriends.Where(f => f.user_id_requester == userId).ToList();

            var sendersIds = sended.Select(f => f.user_id_requester).ToList();
            var sendersUsers = await userRepository.GetUsersByIdsAsync(sendersIds);

            return CombineFriendResponseData(sended, sendersUsers);
        }

        public async Task<FriendResponse> CreateFriendAsync(CreateFriendRequest request, string requesterId) {
            var friendUser = await userRepository.GetUserByPhoneAsync(request.phone)
                ?? throw new UnprocessableException("Пользователь с таким номером телефона не найден");

            if (friendUser.id.ToString() == requesterId) {
                throw new InvalidActionException("Невозможно отправить запрос самому себе");
            }

            var existCheck = await repository.GetFriendByUserAsync(friendUser, requesterId);
            if (existCheck != null) {
                if (existCheck.status == (int)FriendStatusEnum.Rejected) {
                    var updateRequest = mapper.Map<UpdateFriendRequest>(request);
                    updateRequest.status = (int)FriendStatusEnum.Send;

                    return await UpdateFriendAsync(updateRequest, requesterId);
                }

                if (existCheck.user_id_receiver.ToString() == requesterId) {
                    var updateRequest = mapper.Map<UpdateFriendRequest>(request);
                    updateRequest.status = (int)FriendStatusEnum.Accepted;

                    return await UpdateFriendAsync(updateRequest, requesterId);
                }

                throw new InvalidActionException("Вы уже отправили запрос этому пользователю");
            }

            var friend = new Friend {
                user_id_requester = Guid.Parse(requesterId),
                user_id_receiver = friendUser.id
            };
            friend = await repository.CreateFriendAsync(friend);

            var response = mapper.Map<FriendResponse>(friend);
            response.fname = friendUser.fname;
            response.phone = friendUser.phone;
            response.utc_offset = friendUser.utc_offset;

            return response;
        }

        public async Task<FriendResponse> UpdateFriendAsync(UpdateFriendRequest request, string requesterId) {
            var friendUser = await userRepository.GetUserByPhoneAsync(request.phone)
                ?? throw new UnprocessableException("Пользователь с таким номером телефона не найден");

            if (friendUser.id.ToString() == requesterId) {
                throw new InvalidActionException("Невозможно обновить несуществующий запрос дружбы самому себе");
            }

            var friend = await repository.GetFriendByUserAsync(friendUser, requesterId)
                ?? throw new UnprocessableException("Запрос дружбы не найден");

            if (!Enum.IsDefined(typeof(FriendStatusEnum), request.status!)) {
                throw new NotFoundException("Статус не определен");
            }

            Guid userId = Guid.Parse(requesterId);

            if (userId == friend.user_id_requester && request.status == (int)FriendStatusEnum.Accepted) {
                throw new AccessDeniedException("Отправитель не может подтвердить запрос");
            }

            friend.status = (int)request.status!;
            friend = await repository.UpdateFriendAsync(friend);

            var response = mapper.Map<FriendResponse>(friend);
            response.fname = friendUser.fname;
            response.phone = friendUser.phone;
            response.utc_offset = friendUser.utc_offset;

            return response;
        }

        public async Task RemoveFriendAsync(RemoveFriendRequest request, string requesterId) {
            var friendUser = await userRepository.GetUserByPhoneAsync(request.phone)
               ?? throw new UnprocessableException("Пользователь с таким номером телефона не найден");

            if (friendUser.id.ToString() == requesterId) {
                throw new InvalidActionException("Невозможно удалить несуществующий запрос дружбы самому себе");
            }

            var friend = await repository.GetFriendByUserAsync(friendUser, requesterId)
                ?? throw new UnprocessableException("Запрос дружбы не найден");

            await repository.RemoveFriendAsync(friend);
        }

        public async Task UpdateUtcOffsetAsync(UpdateUtcOffsetRequest request, string requesterId) {
            var user = await userRepository.GetUserByIdAsync(requesterId)
                ?? throw new NotFoundException();

            user.utc_offset = request.utc_offset;
            user = await userRepository.UpdateUserAsync(user);
        }
    }
}
