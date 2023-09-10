using MVCExamProject.Models;

namespace MVCExamProject.Repository.Interfaces
{
    public interface IContactUsRepository : IRepository<ContactUs>
    {
        public int count();
    }
}
