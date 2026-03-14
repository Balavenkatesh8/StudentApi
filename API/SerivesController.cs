using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.API;
using StudentApi.Models;

namespace StudentApi.API
{
    [Authorize(Roles = "Admin")]
    [Route("/[controller]")]
    [ApiController]
    public class SerivesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SerivesController(AppDbContext context)
        {
            _context = context;
        }

        //  CREATE 
        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] Serives model)
        {
            if (model == null)
                return BadRequest();

            model.Id = Guid.NewGuid();
            model.CreatedAt = DateTime.UtcNow;

            _context.serives.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(Guid id, [FromBody] Serives model)
        {
            var existingMenu = await _context.serives.FindAsync(id);

            if (existingMenu == null)
                return NotFound("Menu not found");

            existingMenu.serives = model.serives;
            existingMenu.DisplayOrder = model.DisplayOrder;
            existingMenu.IsActive = model.IsActive;

            await _context.SaveChangesAsync();

            return Ok(existingMenu);
        }

        //  DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            var menu = await _context.serives.FindAsync(id);

            if (menu == null)
                return NotFound("Menu not found");

            _context.serives.Remove(menu);
            await _context.SaveChangesAsync();

            return Ok("Menu deleted successfully");
        }
    }
}
