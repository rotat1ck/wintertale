namespace AuthService.Application.DTOs.Responses {
    public class RefreshTokenResponse {
        public string token { get; set; }
        public string refresh_token { get; set; }
        public DateTime expires_at { get; set; }
    }
}
