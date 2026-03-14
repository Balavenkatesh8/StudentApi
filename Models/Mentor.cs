using Microsoft.VisualBasic;

namespace StudentApi.Models
{
    public class Mentor
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? phone { get; set; }
        public string? Specialization { get; set; }
        public DateTime CreatedAt { get; set; }
        

    }
}
