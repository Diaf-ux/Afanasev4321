using Microsoft.AspNetCore.Mvc;
using Afanasev4321.Models;
using Microsoft.EntityFrameworkCore;
using Afanasev4321.DTOs;

namespace Afanasev4321.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly UniversityDbContext _context;

        public DepartmentController(UniversityDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            // Преобразуем сущность в DTO перед возвратом
            var departmentDTO = new DepartmentDTO
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };

            return Ok(departmentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();

            // Преобразуем каждый объект `Department` в `DepartmentDTO`
            var departmentDTOs = departments.Select(d => new DepartmentDTO
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName
            }).ToList();

            return Ok(departmentDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Преобразуем DTO в сущность `Department`
            var department = new Department
            {
                DepartmentName = departmentDTO.DepartmentName,
                TeacherHeaderId = null // Можно указать значение, если известно
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DepartmentId }, department);
        }
    }
}
