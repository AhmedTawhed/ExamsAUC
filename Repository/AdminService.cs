using MVCExamProject.Data;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;

namespace MVCExamProject.Repository
{
    public class AdminService : IAdminRepository
    {
        private readonly ExamContext context;

        public AdminService(ExamContext context)
        {
            this.context = context;
        }
        public void Delete(User t)
        {
            context.Users.Remove(t);
        }

        public bool Find(string email, string password)
        {
            var user = context.Users.FirstOrDefault(a => a.Email == email && a.Password == password);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public User GetAdmin(string email, string password)
        {
            return context.Users.FirstOrDefault(a => a.Email == email && a.Password == password);
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(int id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Insert(User t)
        {
            context.Users.Add(t);
            context.SaveChanges();
        }

        public void Update(User t)
        {
            context.Users.Update(t);
            context.SaveChanges();
        }
    }
}
