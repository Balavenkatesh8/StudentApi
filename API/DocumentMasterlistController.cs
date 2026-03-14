using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace StudentApi.API
{
    [Authorize(Roles = "Admin")]
    [Route("/[controller]")]
    [ApiController]
    public class DocumentMasterlistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentMasterlistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var document = await _context.DocumentMasterlist
                .FirstOrDefaultAsync(x => x.Id == id && x.Is_Active);

            if (document == null)
                return NotFound("Document not found");

            return Ok(document);
        }

      
        [HttpPost]
        public async Task<IActionResult> Create(DocumentMasterlist model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Document_Name))
                    return BadRequest("Document Name is required");

                model.Id = Guid.NewGuid();
                model.Created_At = DateTime.UtcNow;
                model.Is_Active = true;

                await _context.DocumentMasterlist.AddAsync(model);
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

    }
}
