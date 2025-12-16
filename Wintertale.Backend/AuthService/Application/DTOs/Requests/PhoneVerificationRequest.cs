using System.ComponentModel.DataAnnotations;
using AuthService.Application.Common;

namespace AuthService.Application.DTOs.Requests {
    public class PhoneVerificationRequest {
        [Required(ErrorMessage = "Поле phone обязательно")]
        [RegularExpression(Constants.Phone, ErrorMessage = "Не верный формат номера, пример: 74999999999")]
        public string phone { get; set; }
    }
}
