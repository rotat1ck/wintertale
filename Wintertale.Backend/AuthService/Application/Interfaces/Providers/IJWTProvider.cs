using Domain.Models;

namespace AuthService.Application.Interfaces.Providers {
    public interface IJWTProvider {
        string GenerateToken(User user);
    }
}
