using MVCExamProject.Data;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;

namespace MVCExamProject.Repository
{
    public class QuestionOptionService : IQuestionOptionRepository
    {
        private readonly ExamContext context;

        public QuestionOptionService(ExamContext context)
        {
            this.context = context;
        }
        public void Delete(QuestionOption t)
        {
            context.QuestionOptions.Remove(t);
            context.SaveChanges();
        }

        public List<QuestionOption> GetAll()
        {
            return context.QuestionOptions.ToList();
        }

        public QuestionOption GetById(int id)
        {
            return context.QuestionOptions.FirstOrDefault(q => q.Id == id);
        }

        public void Insert(QuestionOption t)
        {
            context.QuestionOptions.Add(t);
            context.SaveChanges();
        }

        public void Update(QuestionOption t)
        {
            context.QuestionOptions.Update(t);
            context.SaveChanges();
        }

        public List<QuestionOption> getForQuestionsList(List<ExamQuestion> questions)
        {
            var options = new List<QuestionOption>();
            foreach (ExamQuestion question in questions)
            {
                var option = context.QuestionOptions.Where(o => o.ExamQuestionId == question.Id).ToList();
                options.AddRange(option);
            }

            return options;   
        }

        public int GetIdByTitle(string title)
        {
            var questionOption = context.QuestionOptions.FirstOrDefault(q => q.Title == title);
            return questionOption?.Id ?? 0;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
