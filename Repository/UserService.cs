using MVCExamProject.Data;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;

namespace MVCExamProject.Repository
{
    public class UserService : IUserRepository
    {
        private readonly ExamContext context;

        public UserService(ExamContext context)
        {
            this.context = context;
        }

    
        public bool Find(string email, string password)

        {
            User user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password );    
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public User GetByUserName(string name)
        {
            return context.Users.FirstOrDefault(u => u.Name == name);
        }

        public string GetRole(int id)
        {
            return "user";
        }

        public User GetUserByEmailAndPassword(string email, string Password)
        {
            return context.Users.FirstOrDefault(u => u.Email == email && u.Password == Password); 
        }

        public List<User> searchByName(string name)
        {
            return context.Users.Where(n=>n.Name.Contains(name)).ToList(); 
        } 

    









        public void Delete(User t)
        {
            context.Users.Remove(t);
            context.SaveChanges();

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
