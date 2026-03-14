namespace StudentApi.Models
{
    public class Serives
    {
     
            public Guid Id { get; set; }
            public string? serives { get; set; }
        
            public int DisplayOrder { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
        
    }
}
