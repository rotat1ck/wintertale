namespace AuthService.Application.Interfaces.Providers {
    public interface IHashProvider {
        string GetHash(string toHash);
        bool Verify(string toVerify, string hashed);
    }
}
