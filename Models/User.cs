using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace MVCExamProject.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(25)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(40)]
        public int? Age { get; set; }
        public bool IsAdmin { get; set; } = false;
        public List<UserExam>? UserExams { get; set; } = new List<UserExam>();
    }
}
