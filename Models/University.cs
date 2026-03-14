namespace StudentApi.Models
{
    public class University
    {
        public Guid Id { get; set; }
        public string University_Name { get; set; } = string.Empty;
        public string? University_Code { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public string? Address { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public bool Is_Active { get; set; }

        public DateTime Created_Date { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }

        public ICollection<Branch> Branch { get; set; }
    }

}
