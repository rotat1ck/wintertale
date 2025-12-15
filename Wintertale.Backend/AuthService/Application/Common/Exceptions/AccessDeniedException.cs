namespace AuthService.Application.Common.Exceptions {
    public class AccessDeniedException : Exception {
        public AccessDeniedException() : base() { }
        public AccessDeniedException(string messgae) : base(messgae) { }
    }
}
