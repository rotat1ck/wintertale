using AuthService.Domain.Enums;

namespace AuthService.Domain.Models {
    public class Verification {
        public Guid id { get; set; } = Guid.NewGuid();
        public string check_id { get; set; }
        public string phone { get; set; }
        public int status { get; set; } = (int)VerificationEnum.Pending;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
