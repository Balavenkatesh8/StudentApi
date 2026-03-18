using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentApi.DTO;
using StudentApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentApi.API
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == model.RoleName);

            if (role == null)
                return BadRequest("Invalid Role");

            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = model.Username,
                Email = model.Email,
                Phoneno = model.Phoneno.ToString(),
                RoleId = role.Id
            };

            user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User Created Successfully");
        }

        // LOGIN
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Username == model.Username);

            if (user == null)
                return Unauthorized("Invalid Username or Password");

            var passwordHasher = new PasswordHasher<User>();

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                model.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid Username or Password");

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        // JWT TOKEN
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ADMIN API
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-data")]
        public IActionResult AdminData()
        {
            return Ok("Admin Access Only");
        }

        // STUDENT API
        [Authorize(Roles = "Student")]
        [HttpGet("student-data")]
        public IActionResult StudentData()
        {
            return Ok("Student Access Only");
        }
    }
}
