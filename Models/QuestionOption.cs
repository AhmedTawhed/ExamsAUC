using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCExamProject.Models
{
    public class QuestionOption
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsRight { get; set; }
        [ForeignKey(nameof(ExamQuestion))]
        public int ExamQuestionId { get; set; }
        public ExamQuestion? ExamQuestion { get; set; }
    }
}
