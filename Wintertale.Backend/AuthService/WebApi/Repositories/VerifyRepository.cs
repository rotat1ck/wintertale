using AuthService.Application.Interfaces.Repositories;
using AuthService.Domain.Models;
using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.WebApi.Repositories {
    public class VerifyRepository : IVerifyRepository {
        private readonly AppDbContext context;

        public VerifyRepository(AppDbContext context) {
            this.context = context;
        }

        public async Task<Verification?> GetVerificationByCheckIdAsync(string checkId) {
            return await context.Verifications.FirstOrDefaultAsync(v => v.check_id == checkId);
        }

        public async Task<Verification?> GetVerificationByPhoneAsync(string phone) {
            return await context.Verifications.FirstOrDefaultAsync(v => v.phone == phone);
        }

        public async Task<Verification> CreateVerificationAsync(Verification verification) {
            context.Verifications.Add(verification);
            await context.SaveChangesAsync();
            return verification;
        }

        public async Task<Verification> UpdateVerificationAsync(Verification verification) {
            verification.updated_at = DateTime.UtcNow;
            context.Verifications.Update(verification);
            await context.SaveChangesAsync();
            return verification;
        }
    }
}
