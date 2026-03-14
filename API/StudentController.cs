using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace StudentApi.API
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("Students")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getAction()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid Student Id"));

            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(new ApiResponse(404, "Student not found"));
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Validation Failed", ModelState));
            student.Id = Guid.NewGuid();
            student.Created_At = DateTime.UtcNow;

            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok(student);


        }

   
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult UpdateStudent(Guid id, [FromBody] Student students)
        {
           
            var existingStudents = _context.Students.FirstOrDefault(s => s.Id == id);
            if (existingStudents == null)
            {
                return NotFound("Student not found");
            }

            // Update fields
            existingStudents.First_Name = students.First_Name;
            existingStudents.Email = students.Email;
            existingStudents.Mobile_Number = students.Mobile_Number;
            existingStudents.Gender = students.Gender;
            existingStudents.Last_Name = students.Last_Name;
            existingStudents.Has_Passport = students.Has_Passport;
            existingStudents.Passport_Number = students.Passport_Number;
            existingStudents.Passport_Country = students.Passport_Country;
            existingStudents.Passport_Expiry_Date = students.Passport_Expiry_Date;
            
            _context.SaveChanges();

            return Ok(existingStudents);
           
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid Student Id"));

            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound(new ApiResponse(404, "Student not found"));

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Ok(new ApiResponse(200, "Student deleted successfully"));
        }

        
    }
}




   


    