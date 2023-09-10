using MVCExamProject.Models;

namespace MVCExamProject.Repository.Interfaces
{
    public interface IUserExamRepository : IRepository<UserExam>
    {
        public UserExam getExamByUserId(int userId);
        public void Save();
        public List<UserExam> getExamByUserName(string userName);


    }
}
