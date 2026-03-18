using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApi.Models
{
    [Table("Document_Master_list")]
    public class DocumentMasterlist
    {

        public Guid Id { get; set; }
       
        public required string Document_Name { get; set; }
        public string? Document_Type { get; set; }
        public bool Is_Mandatory { get; set; }
        public bool Is_Active { get; set; }
        public DateTime Created_At { get; set; }
        public string? Created_By { get; set; }
    }
}

