using Wintertale.Client.Common.DTOs.Requests.Auth;
using Wintertale.Client.Common.DTOs.Responses.Auth;

namespace Wintertale.Client.Services.Auth {
    internal interface IAuthService {
        Task<LoginResponse> RefreshTokenAsync();
        Task<LoginResponse> LoginAsync(LoginRequest request);
        IAsyncEnumerable<string> VerifyAsync(PhoneVerificationRequest request, CancellationToken cancellationToken = default);
        Task<LoginResponse> RegisterAsync(RegisterRequest request);
    }
}
