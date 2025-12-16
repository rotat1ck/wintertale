namespace AuthService.Domain.Models {
    public class Verification {
        public Guid id { get; set; }
        public string check_id { get; set; }
        public string phone { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
