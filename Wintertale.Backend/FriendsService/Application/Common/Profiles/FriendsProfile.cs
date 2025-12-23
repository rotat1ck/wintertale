using AutoMapper;
using Domain.Models;
using FriendsService.Application.DTOs.Requests;
using FriendsService.Application.DTOs.Responses;

namespace FriendsService.Application.Common.Profiles {
    public class FriendsProfile : Profile {
        public FriendsProfile() {
            CreateMap<CreateFriendRequest, Friend>();
            CreateMap<CreateFriendRequest, UpdateFriendRequest>();
            CreateMap<UpdateFriendRequest, Friend>();

            CreateMap<Friend, FriendResponse>();
            CreateMap<User, FriendResponse>();
        }
    }
}
