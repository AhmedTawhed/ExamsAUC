using Microsoft.EntityFrameworkCore;
using MVCExamProject.Data;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;

namespace MVCExamProject.Repository
{
    public class ExamQuestionService : IExamQuestionRepository
    {
        private readonly ExamContext context;

        public ExamQuestionService(ExamContext context)
        {
            this.context = context;
        }
        public void Delete(ExamQuestion t)
        {
            context.ExamQuestions.Remove(t);
            context.SaveChanges();
        }

        public List<ExamQuestion> GetAll()
        {
            return context.ExamQuestions.ToList();
        }

        public ExamQuestion GetById(int id)
        {
            return context.ExamQuestions.FirstOrDefault(u => u.Id == id);
        }

        public void Insert(ExamQuestion t)
        {
            context.ExamQuestions.Add(t);
            context.SaveChanges();
        }

        public void Update(ExamQuestion t)
        {
            context.ExamQuestions.Update(t);
            context.SaveChanges();
        }

        public List<ExamQuestion> getByExamId(int examId)
        {
            return context.ExamQuestions
                .Where(e => e.ExamId == examId)
                .ToList();
        }
    }
}
