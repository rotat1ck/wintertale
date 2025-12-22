using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models {
    public class User {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        [Column(TypeName = "character varying(24)")]
        public string fname { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public double utc_offset { get; set; } = 0;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
