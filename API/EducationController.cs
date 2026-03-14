using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentApi.API
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("student-education")]
    public class StudentEducationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentEducationController(AppDbContext context)
        {
            _context = context;
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var education = await _context.StudentEducations
                .FirstOrDefaultAsync(x => x.Id == id);

            if (education == null)
                return NotFound("Student Education not found");

            return Ok(education);
        }

        


        //  CREATE
        [HttpPost]
        public async Task<IActionResult> Create(StudentEducation education)
        {
            if (education == null)
                return BadRequest("Invalid data");

            if (education.Id == Guid.Empty)
                return BadRequest("StudentId is required");

            // Check Student exists (IMPORTANT - Foreign Key Safety)
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == education.Id);

            

            try
            {
                education.Id = Guid.NewGuid();
                education.Created_At = DateTime.UtcNow;

                await _context.StudentEducations.AddAsync(education);
                await _context.SaveChangesAsync();

                return Ok(education);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error while creating Student Education",
                    Error = ex.Message
                });
            }



        }
    }

    
}
