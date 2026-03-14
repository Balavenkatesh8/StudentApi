using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentApi.Models
{
    public class Branch
    {
        public Guid Branch_Id { get; set; }  // Primary Key

        public Guid Id { get; set; }  // University FK

        [ForeignKey("Id")]
        [JsonIgnore]
        public University? University { get; set; }

        public string? Branch_Name { get; set; }
        public string? Branch_Code { get; set; }
        public bool Is_Active { get; set; }

        public DateTime Created_Date { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        
    }

}
