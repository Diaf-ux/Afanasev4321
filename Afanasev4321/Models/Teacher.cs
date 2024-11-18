using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Afanasev4321.Models
{
    public class Teacher
    {
        [Key] // Первичный ключ
        public int TeachersId { get; set; }
        public required string FirstName { get; set; } // Обязательное поле
        public required string LastName { get; set; } // Обязательное поле
        public required string MiddleName { get; set; } // Обязательное поле
        public int DepartmentId { get; set; }
        public required Department Department { get; set; } // Обязательное поле
        public required string Degree { get; set; } // Обязательное поле
        public required string Position { get; set; } // Обязательное поле
    }
}
