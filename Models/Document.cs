using System.ComponentModel.DataAnnotations;

public class StudentDocument
{
    public Guid Id { get; set; }

    [Required]
    public Guid Student_Id { get; set; }

    [Required]
    public Guid Document_Master_Id { get; set; }

    [Required]
    public string? File_Name { get; set; }

    [Required]
    public string? File_Path { get; set; }

    [Range(1, long.MaxValue)]
    public long File_Size { get; set; }

    public bool Is_Verified { get; set; } = false;

    public DateTime Uploaded_At { get; set; } = DateTime.UtcNow;

    public string? Uploaded_By { get; set; }

    public DateTime Last_Modified_At { get; set; } = DateTime.UtcNow;

    public string? LastModified_By { get; set; }
}