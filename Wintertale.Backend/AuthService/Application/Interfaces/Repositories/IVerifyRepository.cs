using Domain.Models;

namespace AuthService.Application.Interfaces.Repositories {
    public interface IVerifyRepository {
        Task<Verification?> GetVerificationByCheckIdAsync(string checkId);
        Task<Verification?> GetVerificationByPhoneAsync(string phone);
        Task<Verification> CreateVerificationAsync(Verification verification);
        Task<Verification> UpdateVerificationAsync(Verification verification);
    }
}
