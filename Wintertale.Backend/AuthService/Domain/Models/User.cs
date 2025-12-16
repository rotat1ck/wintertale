namespace AuthService.Domain.Models {
    public class User {
        public Guid id { get; set; } = Guid.NewGuid();
        public string fname { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public double utc_offset { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
