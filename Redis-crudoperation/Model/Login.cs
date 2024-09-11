namespace crudoperation.Model
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserSession
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class Register
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int MobileNumber { get; set; }
        public string Password { get; set; }
    }
}
