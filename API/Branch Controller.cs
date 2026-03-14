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
    public class BranchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BranchController(AppDbContext context)
        {
            _context = context;
        }

        //  GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Branch.ToListAsync();
            return Ok(data);
        }

        //  CREATE
        [HttpPost]
        public async Task<IActionResult> Create(BranchCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            if (dto.Id == Guid.Empty)
                return BadRequest("University Id is required");

            //  Check University Exists
            var university = await _context.University
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (university == null)
                return NotFound("University not found");

            //  Duplicate Branch Code Check
            var branchExists = await _context.Branch
                .AnyAsync(b => b.Branch_Code == dto.Branch_Code);

            if (branchExists)
                return Conflict("Branch code already exists");

            try
            {
                var branch = new Branch
                {
                    Branch_Id = Guid.NewGuid(),
                    Branch_Name = dto.Branch_Name,
                    Branch_Code = dto.Branch_Code,
                    Id = dto.Id,   //  Correct FK
                    Is_Active = true,
                    Created_Date = DateTime.UtcNow,
                    CreatedBy = "Admin",            
                    LastModifiedAt = null,           
                    LastModifiedBy = null

                };

                await _context.Branch.AddAsync(branch);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAll), new { id = branch.Branch_Id }, branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error creating branch",
                    Error = ex.Message
                });
            }
        }

        //  DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var branch = await _context.Branch
                .FirstOrDefaultAsync(x => x.Branch_Id == id);

            if (branch == null)
                return NotFound("Branch not found");

            try
            {
                _context.Branch.Remove(branch);
                await _context.SaveChangesAsync();

                return Ok("Branch deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error deleting branch",
                    Error = ex.Message
                });
            }
        }
    }





}


