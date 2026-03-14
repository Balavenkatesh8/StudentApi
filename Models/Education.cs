using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

public class StudentEducation
{
    public Guid Id { get; set; }
    public string Education_Level { get; set; }
   
    public string courses { get; set; }
    public string InstitutionName { get; set; }
    public int YearofPassing { get; set; }
    public String status { get; set; }
     public decimal PercentageOfPassing { get; set; }
    public  DateTime startDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Created_At { get; set; }
    public string? Created_By { get; set; }
    public DateTime? Last_Modified_At { get; set; }
    public string? Last_Modified_By { get; set; }
 
    
}
