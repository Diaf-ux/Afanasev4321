using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Afanasev4321.Models
{
    public class Teacher
    {
        [Key] // Primary Key
        public int TeachersId { get; set; }

        [Required] // Required field in the database
        public string FirstName { get; set; } = string.Empty; // Required field when creating an object

        [Required] // Required field in the database
        public string LastName { get; set; } = string.Empty; // Required field when creating an object

        [Required] // Required field in the database
        public string MiddleName { get; set; } = string.Empty; // Required field when creating an object

        [Required] // Reference to department via identifier
        public int DepartmentId { get; set; }

        [JsonIgnore] // Ignore during JSON serialization to avoid circular reference
        [Required] // Required field in the database
        public Department? Department { get; set; } // Nullable navigation property to the Department

        [Required] // Required field in the database
        public string Degree { get; set; } = string.Empty; // Required field when creating an object

        [Required] // Required field in the database
        public string Position { get; set; } = string.Empty; // Required field when creating an object
    }
}
