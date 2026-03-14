using System.ComponentModel.DataAnnotations;

namespace StudentApi.DTO
{
    public class AddressCreateDto
    {
        
        public Guid Address_Id { get; set; }
        public Guid Id { get; set; }
        public string AddressType { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
       
    }
}
