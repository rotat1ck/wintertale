namespace FriendsService.Application.DTOs.Responses {
    public class FriendResponse {
        public Guid user_id_requester { get; set; }
        public Guid user_id_receiver { get; set; }
        public int status { get; set; }
        public string fname { get; set; }
        public string phone { get; set; }
    }
}
