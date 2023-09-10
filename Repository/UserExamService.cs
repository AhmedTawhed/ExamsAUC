using MVCExamProject.Data;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;

namespace MVCExamProject.Repository
{
    public class UserExamService : IUserExamRepository
    {
        private readonly ExamContext context;

        public UserExamService(ExamContext context)
        {
            this.context = context;
        }

        public void Delete(UserExam t)
        {
            context.UserExams.Remove(t);
            context.SaveChanges();
        }

        public List<UserExam> GetAll()
        {
            return context.UserExams.ToList();
        }

        public UserExam GetById(int id)
        {
            return context.UserExams.FirstOrDefault(u => u.Id == id);
        }

        public UserExam getExamByUserId(int userId)
        {
            return context.UserExams.FirstOrDefault(e => e.UserId == userId);
        }

        public void Insert(UserExam t)
        {
            context.UserExams.Add(t);
            context.SaveChanges();

        }

        public void Update(UserExam t)
        {
            context.UserExams.Update(t);
            context.SaveChanges();

        }

        public List<UserExam> getExamByUserName(string userName)
        {
            
            return context.UserExams.Where(e => e.User.Name.Contains(userName)).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
