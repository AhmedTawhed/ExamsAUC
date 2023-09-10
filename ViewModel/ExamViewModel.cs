using MVCExamProject.Models;

namespace MVCExamProject.ViewModel
{
    public class ExamViewModel
    {
        public int? examId { get; set; }
        public string Title { get; set; }
        public string QuestionsCount { get; set; }
    }
}