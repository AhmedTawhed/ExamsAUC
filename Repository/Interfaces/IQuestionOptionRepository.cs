using MVCExamProject.Models;

namespace MVCExamProject.Repository.Interfaces
{
    public interface IQuestionOptionRepository : IRepository<QuestionOption>
    {
        public List<QuestionOption> getForQuestionsList(List<ExamQuestion> questions);
        public void SaveChanges();
        public int GetIdByTitle(string title);

    }
}
