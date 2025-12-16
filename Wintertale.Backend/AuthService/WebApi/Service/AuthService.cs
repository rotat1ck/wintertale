using System.Security.Cryptography;
using AuthService.Application.Common.Exceptions;
using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Responses;
using AuthService.Application.Interfaces.Providers;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Services;
using AuthService.Domain.Models;
using AutoMapper;

namespace AuthService.WebApi.Service {
    public class AuthService : IAuthService {
        private readonly IAuthRepository repository;
        private readonly IVerifyRepository verifyRepository;
        private readonly IHashProvider hashProvider;
        private readonly IJWTProvider jwtProvider;
        private readonly IMapper mapper;

        public AuthService(IAuthRepository repository, IVerifyRepository verifyRepository, IHashProvider hashProvider, IJWTProvider jwtProvider, IMapper mapper) {
            this.repository = repository;
            this.verifyRepository = verifyRepository;
            this.hashProvider = hashProvider;
            this.jwtProvider = jwtProvider;
            this.mapper = mapper;
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request) {
            var refreshToken = await repository.GetRefreshTokenAsync(request.refresh_token);
            if (refreshToken == null || refreshToken.expires_at > DateTime.UtcNow) {
                throw new UnauthorizedAccessException();
            }



            string refreshToken


        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request) {
            var user = await repository.GetUserByPhoneAsync(request.phone);
            if (user == null) {
                throw new NotFoundException("Пользователь с таким номером телефона не найден");
            }

            if (!hashProvider.Verify(request.password, user.password)) {
                throw new UnauthorizedAccessException("Неверный номер телефона или пароль");
            }

            var response = mapper.Map<LoginResponse>(user);
            response.token = jwtProvider.GenerateToken(user);
            return response;
        }

        public async Task<LoginResponse> RegisterAsync(RegisterRequest request) {
            var phoneCheck = await repository.GetUserByPhoneAsync(request.phone);
            if (phoneCheck != null) {
                throw new InvalidActionException("Пользователь с таким номером телефона уже существует");
            }

            if (await verifyRepository.GetVerificationByPhoneAsync(request.phone) == null) {
                throw new UnauthorizedAccessException("Номер телефона не верифицирован");
            }

            var user = mapper.Map<User>(request);
            user.password = hashProvider.GetHash(user.password);

            var createdUser = await repository.CreateUserAsync(user);
            var response = mapper.Map<LoginResponse>(createdUser);
            response.token = jwtProvider.GenerateToken(createdUser);

            var refreshToken = new RefreshToken {
                user_id = createdUser.id,
                refresh_token = GenerateRefreshToken(),
                expires_at =
            };
            await repository.CreateRefreshTokenAsync

            return response;
        }

        private string GenerateRefreshToken() {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return hashProvider.GetHash(Convert.ToBase64String(randomNumber));
        }
    }
}
