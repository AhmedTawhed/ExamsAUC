using MVCExamProject.Models;

namespace MVCExamProject.Repository.Interfaces
{
    public interface IExamQuestionRepository : IRepository<ExamQuestion>
    {
        public List<ExamQuestion> getByExamId(int examId);
    }
}
