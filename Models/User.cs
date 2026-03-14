namespace StudentApi.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string? Username { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; } = string.Empty;
        public string? Phoneno { get; set; } 

        // Foreign Key
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
    }

}
