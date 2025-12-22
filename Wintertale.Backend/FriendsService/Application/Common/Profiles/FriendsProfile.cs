using AutoMapper;
using FriendsService.Application.DTOs.Requests;
using FriendsService.Application.DTOs.Responses;
using Domain.Models;

namespace FriendsService.Application.Common.Profiles {
    public class FriendsProfile : Profile {
        public FriendsProfile() {
            CreateMap<CreateFriendRequest, Friend>();
            CreateMap<CreateFriendRequest, UpdateFriendRequest>();
            CreateMap<UpdateFriendRequest, Friend>();

            CreateMap<Friend, FriendResponse>();
            CreateMap<User, FriendResponse>().ForSourceMember(u => u.id, opt => opt.DoNotValidate());
        }
    }
}
