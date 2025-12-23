using System.ComponentModel.DataAnnotations;

namespace FriendsService.Application.DTOs.Requests {
    public class UpdateUtcOffsetRequest {
        [Required(ErrorMessage = "Поле utc_offset обязательно")]
        [Range(-720, 840)]
        public double utc_offset { get; set; }
    }
}
