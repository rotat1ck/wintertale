using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Responses;
using AuthService.Domain.Models;
using AutoMapper;

namespace AuthService.Application.Common.Profiles {
    public class AuthProfile : Profile {
        public AuthProfile() {
            CreateMap<LoginRequest, User>();
            CreateMap<RegisterRequest, User>();
            CreateMap<User, LoginResponse>();
        }
    }
}
