using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.DTO;
using StudentApi.Models;
using System.Threading.Tasks;

namespace StudentApi.API
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AddressController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/address
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Addresses.ToListAsync();
            return Ok(data);
        }

        //Get:api/address/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.Address_Id == id);

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        // POST: api/address
        
        [HttpPost]
        public async Task<IActionResult> Create(AddressCreateDto dto)
        {
            var address = new Address
            {
                Address_Id = Guid.NewGuid(),
                Id = dto.Id,
                Address_Type = dto.AddressType,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                Postal_Code = dto.PostalCode,
                Country = dto.Country,
                Is_Active = true,
                Created_At = DateTime.UtcNow,
                Created_By = "admin"
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return Ok(address);
        }

        // PUT: api/address/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAddressDto dto)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.Address_Id == id);

            if (address == null)
                return NotFound($"Address with Id {id} not found");

            address.Address_Type = dto.Address_Type;
            address.Street = dto.Street;
            address.City = dto.City;
            address.State = dto.State;
            address.Postal_Code = dto.Postal_Code;
            address.Country = dto.Country;
            address.LastModified_At = DateTime.UtcNow;
            address.LastModified_By = "Admin";

            await _context.SaveChangesAsync();

            return Ok(address);
        }
        // DELETE: api/address/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.Address_Id == id);

            if (address == null)
                return NotFound($"Address with Id {id} not found");

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }
    }

}

