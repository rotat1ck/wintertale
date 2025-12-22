using Domain.Enums;

namespace Domain.Models {
    public class Friend {
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid user_id_requester { get; set; }
        public Guid user_id_receiver { get; set; }
        public int status { get; set; } = (int)FriendStatusEnum.Send;
        public DateTime сreated_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
