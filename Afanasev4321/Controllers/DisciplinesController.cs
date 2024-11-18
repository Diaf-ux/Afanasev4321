using Microsoft.AspNetCore.Mvc;
using Afanasev4321.Models;
using Microsoft.EntityFrameworkCore;

namespace Afanasev4321.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly UniversityDbContext _context;

        public DisciplinesController(UniversityDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplines>>> GetDisciplines(
            [FromQuery] int? teacherId, [FromQuery] int? minLoad, [FromQuery] int? maxLoad)
        {
            var query = _context.Disciplines.AsQueryable();

            if (teacherId.HasValue)
            {
                query = query.Where(d => d.TeacherId == teacherId.Value);
            }

            if (minLoad.HasValue && maxLoad.HasValue)
            {
                query = query.Where(d => d.Load >= minLoad.Value && d.Load <= maxLoad.Value);
            }

            return await query.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Disciplines>> CreateDiscipline([FromBody] Disciplines discipline)
        {
            _context.Disciplines.Add(discipline);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDisciplines), new { id = discipline.DisciplineId }, discipline);
        }
    }
}