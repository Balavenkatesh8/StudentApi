using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApi.Models
{
    public class StudentMentorLink
    {
     
            public Guid Id { get; set; }
           
            public Guid StudentId { get; set; }

            public Guid MentorId { get; set; }

            public DateTime CreatedAt { get; set; }

          
        
    }
}
