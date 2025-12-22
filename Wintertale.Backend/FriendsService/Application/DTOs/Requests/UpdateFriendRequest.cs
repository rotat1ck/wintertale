using FriendsService.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace FriendsService.Application.DTOs.Requests {
    public class UpdateFriendRequest {
        [Required(ErrorMessage = "Поле phone обязательно")]
        [RegularExpression(Constants.Phone, ErrorMessage = "Не верный формат номера, пример: 74999999999")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Поле status обязательно")]
        public int? status { get; set; }
    }
}
