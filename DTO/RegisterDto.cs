namespace StudentApi.API
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;   // ✅ ADD THIS

        public string Password { get; set; } = string.Empty;
        public long Phoneno { get; set; }
        

        public string RoleName { get; set; } = string.Empty;
        public string Role { get; set; }
    }

}

