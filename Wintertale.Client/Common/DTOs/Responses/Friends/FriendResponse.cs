namespace Wintertale.Client.Common.DTOs.Responses.Friends {
    internal class FriendResponse {
        public Guid user_id_requester { get; set; }
        public Guid user_id_receiver { get; set; }
        public int status { get; set; }
        public string fname { get; set; }
        public string phone { get; set; }
        public double utc_offset { get; set; }
    }
}
