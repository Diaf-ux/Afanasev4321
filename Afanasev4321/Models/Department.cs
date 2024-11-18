using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Afanasev4321.Models
{
    public class Department
    {
        [Key] // Первичный ключ
        public int DepartmentId { get; set; }
        public required string DepartmentName { get; set; } // Обязательное поле
        public int? TeacherHeaderId { get; set; }
        [JsonIgnore]
        public required Teacher Teacher { get; set; } // Обязательное поле

        public bool isValidDepartmentName()
        {
            return Regex.Match(DepartmentName, @"^[A-ЯЁ][а-яё]*(\s[A-ЯЁ][а-яё]*)?$").Success;
        }
    }
}
