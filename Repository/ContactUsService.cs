using MVCExamProject.Data;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;

namespace MVCExamProject.Repository
{
	public class ContactUsService : IContactUsRepository
	{
		private readonly ExamContext context;

		public ContactUsService(ExamContext context)
		{
			this.context = context;
		}
		public void Delete(ContactUs t)
		{
			context.Remove(t);
			context.SaveChanges();
		}

		public List<ContactUs> GetAll()
		{
			return context.ContactUsMSGS.ToList();
		}

		public ContactUs GetById(int id)
		{
			return context.ContactUsMSGS.FirstOrDefault(c => c.Id == id);
		}

        public void Insert(ContactUs t)
        {
            context.ContactUsMSGS.Add(t);
            context.SaveChanges();
        }

        public void Update(ContactUs t)
        {
            context.ContactUsMSGS.Update(t);
            context.SaveChanges();
        }

		public int count()
		{
			return context.ContactUsMSGS.Count();
		}
	}
}
