using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentApi.API
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("student-document")]
    public class StudentDocumentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentDocumentController(AppDbContext context)
        {
            _context = context;
        }

        //  GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            var document = await _context.StudentDocuments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (document == null)
                return NotFound("Document not found");

            return Ok(document);
        }


        // Post

        [HttpPost]
        [ProducesResponseType(typeof(StudentDocument), 201)]
        public async Task<IActionResult> Create([FromBody] StudentDocument document)
        {
            if (document == null)
                return BadRequest("Document data is required.");

            if (document.Student_Id == Guid.Empty)
                return BadRequest("Student_Id is required.");

            if (document.Document_Master_Id == Guid.Empty)
                return BadRequest("Document_Master_Id is required.");

            // Validate Student FK
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == document.Student_Id);

            if (!studentExists)
                return BadRequest("Invalid Student_Id.");

            //  Validate Document Master FK
            var documentMasterExists = await _context.DocumentMasterlist
                .AnyAsync(d => d.Id == document.Document_Master_Id);

            if (!documentMasterExists)
                return BadRequest("Invalid Document_Master_Id.");

            try
            {
                document.Id = Guid.NewGuid();
                document.Uploaded_At = DateTime.UtcNow;
                document.Last_Modified_At = DateTime.UtcNow;

                await _context.StudentDocuments.AddAsync(document);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = document.Id },
                    document
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Error while creating document",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}


    
