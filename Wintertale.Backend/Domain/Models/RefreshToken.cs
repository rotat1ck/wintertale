using System.ComponentModel.DataAnnotations;

namespace Domain.Models {
    public class RefreshToken {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid user_id { get; set; }
        public string refresh_token { get; set; }
        public DateTime expires_at { get; set; }
    }
}
