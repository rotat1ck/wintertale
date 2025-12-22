using AuthService.Application.DTOs.Requests;
using Domain.Models;

namespace AuthService.Application.Interfaces.Services {
    public interface IVerifyService {
        Task<Verification?> CheckVerificationAsync(PhoneVerificationRequest request);
        Task InitializeVerificationAsync(PhoneVerificationRequest request, StreamWriter writer, CancellationTokenSource cancellationToken);
        Task SubscribeAsync(PhoneVerificationRequest request, StreamWriter writer, CancellationTokenSource cancellationToken);
        Task PublishAsync(string checkId, string callcheck_status);
        Task WriteEventAsync(StreamWriter writer, string eventText);
    }
}
