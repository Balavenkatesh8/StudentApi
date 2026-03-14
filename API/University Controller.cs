using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.DTO;
using StudentApi.Models;

namespace StudentApi.API
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UniversityController(AppDbContext context)
        {
            _context = context;
        }

        //  GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.University.ToListAsync();
            return Ok(data);
        }

        //  CREATE
        [HttpPost]
        public async Task<IActionResult> Create(UniversityCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            if (string.IsNullOrWhiteSpace(dto.University_Code))
                return BadRequest("University Code is required");

            //  Duplicate Check
            var exists = await _context.University
                .AnyAsync(u => u.University_Code == dto.University_Code);

            if (exists)
                return Conflict("University Code already exists");

            try
            {
                var university = new University
                {
                    Id = Guid.NewGuid(),
                    University_Name = dto.University_Name,
                    University_Code = dto.University_Code,
                    Email = dto.Email,
                    Phone_Number = dto.Phone_Number,
                    Address = dto.Address,
                    Address2 = dto.Address2,
                    ZipCode = dto.ZipCode,
                    City = dto.City,
                    State = dto.State,
                    Country = dto.Country,
                    Is_Active = true,
                    Created_Date = DateTime.UtcNow,
                    CreatedBy = dto.CreatedBy
                };

                await _context.University.AddAsync(university);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAll), new { id = university.Id }, university);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error creating university",
                    Error = ex.Message
                });
            }
        }

        //  UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UniversityUpdateDto model)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var data = await _context.University.FindAsync(id);

            if (data == null)
                return NotFound("University not found");

            data.University_Name = model.University_Name;
            data.University_Code = model.University_Code;
            data.Email = model.Email;
            data.Phone_Number = model.Phone_Number;
            data.Address = model.Address;
            data.Address2 = model.Address2;
            data.ZipCode = model.ZipCode;
            data.City = model.City;
            data.State = model.State;
            data.Country = model.Country;
            data.Is_Active = model.Is_Active;
            data.LastModifiedAt = DateTime.UtcNow;
            data.LastModifiedBy = model.LastModifiedBy;

            await _context.SaveChangesAsync();

            return Ok(data);
        }

        //  DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniversity(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var university = await _context.University
                .Include(u => u.Branch)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (university == null)
                return NotFound("University not found");

            try
            {
                if (university.Branch != null && university.Branch.Any())
                {
                    _context.Branch.RemoveRange(university.Branch);
                }

                _context.University.Remove(university);
                await _context.SaveChangesAsync();

                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error deleting university",
                    Error = ex.Message
                });
            }
        }
    }




}








