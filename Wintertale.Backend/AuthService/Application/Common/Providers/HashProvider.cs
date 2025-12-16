using AuthService.Application.Interfaces.Providers;
using Crypto = BCrypt.Net.BCrypt;

namespace AuthService.Application.Common.Providers {
    public class HashProvider : IHashProvider {
        public string GetHash(string toHash) {
            return Crypto.HashPassword(toHash, workFactor: 12);
        }
    }
}
