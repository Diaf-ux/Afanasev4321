namespace Afanasev4321.DTOs
{
    public class TeacherDTO
    {
        public int TeachersId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
}
