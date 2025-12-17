using AuthService.Application.DTOs.Requests;
using AuthService.Domain.Models;

namespace AuthService.Application.Interfaces.Services {
    public interface IVerifyService {
        Task<Verification?> GetVerificationByCheckIdAsync(string checkId);
        Task<Verification?> GetVerificationByPhoneAsync(string phone);
        Task<Verification> CreateVerificationAsync(PhoneVerificationRequest request);
        Task<Verification> UpdateVerificationAsync(PhoneVerificationRequest request);

    }
}
