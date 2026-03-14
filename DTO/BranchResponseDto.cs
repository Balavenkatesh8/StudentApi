namespace StudentApi.DTO
{
    public class BranchResponseDto
    {
        public Guid Branch_Id { get; set; }
        public string? Branch_Name { get; set; }
        public string? Branch_Code { get; set; }

        public Guid Id { get; set; }
        
        public string? University_Name { get; set; }
    }

}
