namespace AuthService.Domain.Models {
    public class RefreshToken {
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid user_id { get; set; }
        public string refresh_token { get; set; }
        public DateTime expires_at { get; set; }
    }
}
