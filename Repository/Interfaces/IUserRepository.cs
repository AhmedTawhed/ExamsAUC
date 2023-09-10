using MVCExamProject.Models;

namespace MVCExamProject.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool Find(string email, string password);
        //void create(User user);
        void Save();
        public User GetByUserName(string name);

        public string GetRole(int id);

        public User GetUserByEmailAndPassword(string email, string Password);
        public List<User> searchByName(string name);
    }
}
