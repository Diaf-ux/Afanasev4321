using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;


namespace Afanasev4321.Models
{
    public class Disciplines
    {
        [Key] // Первичный ключ
        public int DisciplineId { get; set; }
        public required string DisciplineName { get; set; } // Обязательное поле
        public int DepartmentId { get; set; }
        public required Department Department { get; set; } // Обязательное поле
        public int TeacherId { get; set; } // Преподаватель, ведущий дисциплину
        public required Teacher Teacher { get; set; } // Обязательное поле
        public int Load { get; set; } // Нагрузка в часах
    }
}
