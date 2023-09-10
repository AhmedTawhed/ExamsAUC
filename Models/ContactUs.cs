using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCExamProject.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }
        
        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
