using System.Security.Cryptography;
using AuthService.Application.Common.Exceptions;
using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Responses;
using AuthService.Application.Interfaces.Providers;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Services;
using AutoMapper;
using Domain.Models;

namespace AuthService.WebApi.Services {
    public class AuthService : IAuthService {
        private readonly IAuthRepository repository;
        private readonly IVerifyRepository verifyRepository;
        private readonly IHashProvider hashProvider;
        private readonly IJWTProvider jwtProvider;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthService(IAuthRepository repository, IVerifyRepository verifyRepository,
            IHashProvider hashProvider, IJWTProvider jwtProvider, IMapper mapper,
            IConfiguration configuration) {

            this.repository = repository;
            this.verifyRepository = verifyRepository;
            this.hashProvider = hashProvider;
            this.jwtProvider = jwtProvider;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request) {
            var refreshTokenObj = await repository.GetRefreshTokenAsync(request.refresh_token);
            if (refreshTokenObj == null || refreshTokenObj.expires_at < DateTime.UtcNow) {
                throw new UnauthorizedAccessException("Токен невалиден или устарел");
            }

            var user = await repository.GetUserByIdAsync(refreshTokenObj.user_id.ToString())
                ?? throw new NotFoundException();

            refreshTokenObj.refresh_token = GenerateRefreshToken();
            refreshTokenObj.expires_at = DateTime.UtcNow + TimeSpan.FromDays(int.Parse(configuration["RefreshToken:LifetimeDays"]!));
            await repository.UpdateRefreshTokenAsync(refreshTokenObj);

            var response = mapper.Map<LoginResponse>(user);

            response.token = jwtProvider.GenerateToken(user);
            response.refresh_token = refreshTokenObj.refresh_token;
            response.expires_at = refreshTokenObj.expires_at;

            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request) {
            var user = await repository.GetUserByPhoneAsync(request.phone)
                ?? throw new NotFoundException("Пользователь с таким номером телефона не найден");

            if (!hashProvider.Verify(request.password, user.password)) {
                throw new UnauthorizedAccessException("Неверный номер телефона или пароль");
            }

            var response = mapper.Map<LoginResponse>(user);
            var refreshTokenObj = (await repository.GetRefreshTokenByUserIdAsync(user.id.ToString()))!;

            refreshTokenObj.refresh_token = GenerateRefreshToken();
            refreshTokenObj.expires_at = DateTime.UtcNow + TimeSpan.FromDays(int.Parse(configuration["RefreshToken:LifetimeDays"]!));
            await repository.UpdateRefreshTokenAsync(refreshTokenObj);

            response.token = jwtProvider.GenerateToken(user);
            response.refresh_token = refreshTokenObj.refresh_token;
            response.expires_at = refreshTokenObj.expires_at;

            return response;
        }

        public async Task<LoginResponse> RegisterAsync(RegisterRequest request) {
            var phoneCheck = await repository.GetUserByPhoneAsync(request.phone);
            if (phoneCheck != null) {
                throw new UnprocessableException("Пользователь с таким номером телефона уже существует");
            }

            //if (await verifyRepository.GetVerificationByPhoneAsync(request.phone) == null) {
            //    throw new UnauthorizedAccessException("Номер телефона не верифицирован");
            //}

            var user = mapper.Map<User>(request);
            user.password = hashProvider.GetHash(user.password);

            var createdUser = await repository.CreateUserAsync(user);
            var response = mapper.Map<LoginResponse>(createdUser);
            var refreshTokenObj = new RefreshToken {
                user_id = createdUser.id,
                refresh_token = GenerateRefreshToken(),
                expires_at = DateTime.UtcNow + TimeSpan.FromDays(int.Parse(configuration["RefreshToken:LifetimeDays"]!))
            };
            await repository.CreateRefreshTokenAsync(refreshTokenObj);

            response.token = jwtProvider.GenerateToken(createdUser);
            response.refresh_token = refreshTokenObj.refresh_token;
            response.expires_at = refreshTokenObj.expires_at;

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
