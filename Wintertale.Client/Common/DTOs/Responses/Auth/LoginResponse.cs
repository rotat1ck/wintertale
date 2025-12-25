namespace Wintertale.Client.Common.DTOs.Responses.Auth {
    internal class LoginResponse {
        public string token { get; set; }
        public string refresh_token { get; set; }
        public DateTime expires_at { get; set; }
        public Guid id { get; set; }
        public string phone { get; set; }
        public double utc_offset { get; set; }
    }
}
