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
            CreateMap<User, FriendResponse>()
                .ForMember(dest => dest.user_id_requester, opt => opt.Ignore())
                .ForMember(dest => dest.user_id_receiver, opt => opt.Ignore())
                .ForMember(dest => dest.status, opt => opt.Ignore());
        }
    }
}
