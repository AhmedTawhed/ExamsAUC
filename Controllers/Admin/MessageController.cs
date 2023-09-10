using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;
using System.Data;

namespace MVCExamProject.Controllers.Admin
{
	[Authorize(Roles = "Admin")]

	public class MessageController : Controller
	{
		private IContactUsRepository contactUsRepository;

		public MessageController(IContactUsRepository contactUsRepo)
		{
			contactUsRepository
				 = contactUsRepo;
		}


		[Route("admin/messages")]
		public IActionResult Index()
		{
			List<ContactUs> contactUsModel = contactUsRepository.GetAll();
			return View("~/Views/Admin/Message/index.cshtml", contactUsModel);
		}



		[Route("admin/messages/delete")]
		public IActionResult Delete(int id)
		{
			ContactUs SelectedRow = contactUsRepository.GetById(id);

			if (SelectedRow != null)
			{
				contactUsRepository.Delete(SelectedRow);
				return RedirectToAction("Index");
			}
			return View("~/Views/Admin/Message/index.cshtml");
		}





	}
}
