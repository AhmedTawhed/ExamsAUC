using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCExamProject.Models
{
    public class ExamQuestion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam? Exam { get; set; }
        public string QuestionType { get; set; }

        public List<QuestionOption>? Options { get; set; } = new List<QuestionOption>();
    }
}
