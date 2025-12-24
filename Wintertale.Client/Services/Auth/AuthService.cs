using System.Runtime.CompilerServices;
using Wintertale.Client.Common.DTOs.Requests.Auth;
using Wintertale.Client.Common.DTOs.Responses.Auth;
using Wintertale.Client.Services.BaseApi;

namespace Wintertale.Client.Services.Auth {
    internal class AuthService : IAuthService {
        private readonly IApiService api;

        public AuthService(IApiService api) {
            this.api = api;
        }

        public async Task<LoginResponse> RefreshTokenAsync() {
            var refreshToken = await SecureStorage.GetAsync("WintertaleRefreshToken")
                ?? throw new UnauthorizedAccessException("Сессия не найдена");

            var request = new RefreshTokenRequest {
                refresh_token = refreshToken
            };

            var response = await api.HttpAsync<RefreshTokenRequest, LoginResponse>(HttpMethod.Post, "api/v1/auth/refresh", request);

            api.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.token);
            await SecureStorage.SetAsync("WintertaleRefreshToken", response.refresh_token);

            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request) {
            var response = await api.HttpAsync<LoginRequest, LoginResponse>(HttpMethod.Post, "api/v1/auth/login", request);

            api.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.token);
            await SecureStorage.SetAsync("WintertaleRefreshToken", response.refresh_token);

            return response;
        }

        public async IAsyncEnumerable<string> VerifyAsync(PhoneVerificationRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default) {
            await foreach (var message in api.SseAsync("api/v1/auth/verify", request)) {
                yield return message;
            }
        }

        public async Task<LoginResponse> RegisterAsync(RegisterRequest request) {
            var response =  await api.HttpAsync<RegisterRequest, LoginResponse>(HttpMethod.Post, "api/v1/auth/register", request);

            api.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.token);
            await SecureStorage.SetAsync("WintertaleRefreshToken", response.refresh_token);

            return response;
        }
    }
}
