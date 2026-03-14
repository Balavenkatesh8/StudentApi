using StudentApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{

    //[Column("Id")]
    public Guid Id { get; set; }

    public string? First_Name { get; set; }
    public string? Last_Name { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string? Mobile_Number { get; set; }
    public bool Is_Active { get; set; }

    public DateTime Created_At { get; set; }
    public string? Created_By { get; set; }
    public DateTime? Last_Modified_At { get; set; }
    public string? Last_Modified_By { get; set; }
    public bool Has_Passport { get; set; }

    [RegularExpression(@"^[A-Z0-9]{6,9}$",
        ErrorMessage = "Invalid passport format")]
    public string? Passport_Number { get; set; }

    public string? Passport_Country { get; set; }

    public DateTime? Passport_Expiry_Date { get; set; }
}


    



   
