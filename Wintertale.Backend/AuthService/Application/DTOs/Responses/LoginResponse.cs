namespace AuthService.Application.DTOs.Responses {
    public class LoginResponse {
        public string token { get; set; }
        public Guid id { get; set; }
        public string phone { get; set; }
    }
}
