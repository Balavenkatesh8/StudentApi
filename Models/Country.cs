using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Country
{
    [Key]
    [Column("Country_Id")]   
    public Guid CountryId { get; set; }

    [Column("Country_Code")]
    public string CountryCode { get; set; }

    [Column("Country_Name")]
    public string CountryName { get; set; }

    [Column("Country_Phone_Code")]
    public string CountryPhoneCode { get; set; }

    public string? Region { get; set; }
    public string? Description { get; set; }
    public bool Display_At_Start { get; set; }
    public string? Created_By { get; set; }
    public string? Last_Modified_By { get; set; }
   // public bool Is_Active { get; internal set; }
}
