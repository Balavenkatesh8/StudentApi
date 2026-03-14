using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace StudentApi.API
{
    [Route("/[controller]")]
    [ApiController]
    public class MentorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MentorsController(AppDbContext context)
        {
            _context = context;
        }


        // GET ALL MENTORS
        [HttpGet]
        public async Task<IActionResult> GetMentors()
        {
            var mentors = await _context.Mentors.ToListAsync();
            return Ok(mentors);
        }

        // GET MENTOR BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMentor(Guid id)
        {
            var mentor = await _context.Mentors.FindAsync(id);

            if (mentor == null)
                return NotFound();

            return Ok(mentor);
        }
        // CREATE MENTOR
        [HttpPost]
        public async Task<IActionResult> CreateMentor(Mentor model)
        {
            model.Id = Guid.NewGuid();
            model.CreatedAt = DateTime.UtcNow;

            _context.Mentors.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // UPDATE MENTOR
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMentor(Guid id, Mentor model)
        {
            var mentor = await _context.Mentors.FindAsync(id);

            if (mentor == null)
                return NotFound();

            mentor.Name = model.Name;
            mentor.Email = model.Email;
            mentor.phone = model.phone;
            mentor.Specialization = model.Specialization;

            await _context.SaveChangesAsync();

            return Ok(mentor);
        }

        // DELETE MENTOR
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentor(Guid id)
        {
            var mentor = await _context.Mentors.FindAsync(id);

            if (mentor == null)
                return NotFound();

            _context.Mentors.Remove(mentor);
            await _context.SaveChangesAsync();

            return Ok("Mentor Deleted");
        }

        // LINK STUDENT TO MENTOR
        [HttpPost("studentmentorlink")]
        public async Task<IActionResult> LinkStudent(LinkRequest request)
        {
            var student = await _context.Students.FindAsync(request.StudentId);
            var mentor = await _context.Mentors.FindAsync(request.MentorId);

            if (student == null || mentor == null)
                return BadRequest("Student or Mentor not found");

            var exists = await _context.StudentMentorLinks
                .AnyAsync(x => x.StudentId == request.StudentId && x.MentorId == request.MentorId);

            if (exists)
                return BadRequest("Student already linked to this mentor");

            var link = new StudentMentorLink
            {
                Id = Guid.NewGuid(),
                StudentId = request.StudentId,
                MentorId = request.MentorId,
                CreatedAt = DateTime.UtcNow
            };

            _context.StudentMentorLinks.Add(link);
            await _context.SaveChangesAsync();

            return Ok("Student linked to mentor");
        }

        // UNLINK STUDENT FROM MENTOR
        [HttpDelete("studentmentorunlink")]
        public async Task<IActionResult> UnlinkStudent(LinkRequest request)
        {
            var link = await _context.StudentMentorLinks
                .FirstOrDefaultAsync(x => x.StudentId == request.StudentId && x.MentorId == request.MentorId);

            if (link == null)
                return NotFound("Link not found");

            _context.StudentMentorLinks.Remove(link);
            await _context.SaveChangesAsync();

            return Ok("Student unlinked from mentor");
        }
    }
}


