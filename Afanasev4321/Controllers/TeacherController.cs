using Microsoft.AspNetCore.Mvc;
using Afanasev4321.Models;
using Microsoft.EntityFrameworkCore;
using Afanasev4321.DTOs;

namespace Afanasev4321.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly UniversityDbContext _context;

        public TeacherController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: api/Teacher/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            // Находим преподавателя по ID и загружаем связанные данные о кафедре
            var teacher = await _context.Teachers
                .Include(t => t.Department)
                .FirstOrDefaultAsync(t => t.TeachersId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            // Преобразуем в DTO перед отправкой ответа
            var teacherDTO = new TeacherDTO
            {
                TeachersId = teacher.TeachersId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                DepartmentId = teacher.DepartmentId,
                Degree = teacher.Degree,
                Position = teacher.Position
            };

            return Ok(teacherDTO);
        }

        // GET: api/Teacher
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            // Получаем всех преподавателей и загружаем связанные данные о кафедрах
            var teachers = await _context.Teachers
                .Include(t => t.Department)
                .ToListAsync();

            // Преобразуем каждого преподавателя в DTO
            var teacherDTOs = teachers.Select(teacher => new TeacherDTO
            {
                TeachersId = teacher.TeachersId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                DepartmentId = teacher.DepartmentId,
                Degree = teacher.Degree,
                Position = teacher.Position
            }).ToList();

            return Ok(teacherDTOs);
        }

        [HttpGet("filter-by-department/{departmentId}")]
        public async Task<IActionResult> GetTeachersByDepartment(int departmentId)
        {
            // Проверяем наличие кафедры с таким ID
            var departmentExists = await _context.Departments.AnyAsync(d => d.DepartmentId == departmentId);
            if (!departmentExists)
            {
                return NotFound(new { message = "Кафедра с указанным ID не найдена" });
            }

            // Получаем преподавателей, связанных с указанной кафедрой
            var teachers = await _context.Teachers
                .Where(t => t.DepartmentId == departmentId)
                .ToListAsync();

            // Преобразуем в DTO перед отправкой ответа
            var teacherDTOs = teachers.Select(teacher => new TeacherDTO
            {
                TeachersId = teacher.TeachersId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                DepartmentId = teacher.DepartmentId,
                Degree = teacher.Degree,
                Position = teacher.Position
            }).ToList();

            return Ok(teacherDTOs);
        }

        // POST: api/Teacher
        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherDTO teacherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Проверяем, существует ли кафедра с указанным ID
            var department = await _context.Departments.FindAsync(teacherDTO.DepartmentId);
            if (department == null)
            {
                return BadRequest("Department not found.");
            }

            // Создаем новую сущность `Teacher` на основе DTO
            var teacher = new Teacher
            {
                FirstName = teacherDTO.FirstName,
                LastName = teacherDTO.LastName,
                MiddleName = teacherDTO.MiddleName,
                DepartmentId = teacherDTO.DepartmentId,
                Degree = teacherDTO.Degree,
                Position = teacherDTO.Position
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            // Возвращаем созданный объект
            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.TeachersId }, teacherDTO);
        }
    }
}
