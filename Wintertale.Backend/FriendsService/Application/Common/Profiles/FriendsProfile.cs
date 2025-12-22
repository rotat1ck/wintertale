using AutoMapper;
using FriendsService.Application.DTOs.Responses;
using FriendsService.Domain.Models;

namespace FriendsService.Application.Common.Profiles {
    public class FriendsProfile : Profile {
        public FriendsProfile() {
            CreateMap<Friend, FriendResponse>();
            CreateMap<User, FriendResponse>().ForSourceMember(u => u.id, opt => opt.DoNotValidate());
        }
    }
}
