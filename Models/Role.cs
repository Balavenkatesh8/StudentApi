using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApi.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        [Column("Role_Name")]
        public string RoleName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<User> Users { get; set; } = new List<User>();
        
    }

}
