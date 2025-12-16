using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.DTOs.Requests {
    public class RefreshTokenRequest {
        [Required(ErrorMessage = "Поле refresh_token обязательно")]
        public string refresh_token { get; set; }
    }
}
