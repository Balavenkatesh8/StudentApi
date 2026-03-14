using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApi.Models
{
    public class LinkRequest
    {
        
        public Guid StudentId { get; set; }
        public Guid MentorId { get; set; }
    }
}
