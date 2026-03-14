
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Authorize(Roles = "Admin")]
[ApiController]
[Route("/[controller]")]

public class CountryController : ControllerBase
{
    private readonly AppDbContext _context;

    public CountryController(AppDbContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        
        var countries = await _context.Country.ToListAsync();
        return Ok(countries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid Id");
        var country = await _context.Country
                                    .FirstOrDefaultAsync(c => c.CountryId == id);

        if (country == null)
            return NotFound();

        return Ok(country);
    }
    [HttpPost]
    public async Task<IActionResult> Create(Country country)
    {
        if (country == null)
            return BadRequest("Invalid data");

        await _context.Country.AddAsync(country);
        await _context.SaveChangesAsync();

        return Ok(country);
    }
}