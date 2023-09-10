using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCExamProject.Repository;
using MVCExamProject.Repository.Interfaces;

namespace MVCExamProject.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
	{

		private readonly IContactUsRepository contactSerive;
		private readonly IStudentRepository studentService;
        private readonly IExamRepository examService;

        public DashboardController(
			IContactUsRepository _contactService,
			IStudentRepository _studentService,
			IExamRepository _examRepository
		)
		{
			contactSerive = _contactService;
            studentService = _studentService;
            examService = _examRepository;
		}

		[Route("admin/dashboard")]
		public IActionResult Index()
		{
			ViewData["studentsCount"] = studentService.count();
            ViewData["messagesCount"] = contactSerive.count();
            ViewData["examsCount"] = examService.count();
            return View("~/Views/Admin/Dashboard.cshtml");
        }
	}
}
