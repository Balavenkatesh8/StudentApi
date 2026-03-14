using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApi.Models
{
    public class Address
    {
        
        public Guid Address_Id { get; set; }
        
        public Guid Id { get; set; }
        public string? Address_Type { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Postal_Code { get; set; }
        public string? Country { get; set; }
        public bool Is_Active { get; set; }
        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public string? Created_By { get; set; }

        public DateTime? LastModified_At { get; set; }
        public string? LastModified_By { get; set; }

        //public Student? Student { get; set; }
    }

}

