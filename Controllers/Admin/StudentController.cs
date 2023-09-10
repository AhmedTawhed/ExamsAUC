using Microsoft.AspNetCore.Mvc;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Data;


namespace MVCExamProject.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly IUserRepository _userRepository;

        public StudentController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [Route("admin/students")]
        public IActionResult Index()
        {
            List<User> students = _userRepository.GetAll().Where( u =>u.IsAdmin==false).ToList();
            return View("~/Views/Admin/Student/index.cshtml", students);
        }

        public IActionResult viewDetails(int id)
        {
            User student = _userRepository.GetById(id);
            return View(student);
        }

        [Route("admin/students/delete")]
        public IActionResult Delete(int id)
        {
            User student = _userRepository.GetById(id);

            if (student != null)
            {

                _userRepository.Delete(student);
                return RedirectToAction("Index");

            }
            return View("~/Views/Admin/Student/index.cshtml");
        }

        //search
        public IActionResult searchName(string name) { 
            var result = _userRepository.searchByName(name).Where(u => u.IsAdmin == false).ToList();
            return View("~/Views/Admin/Student/index.cshtml", result);
        }



    }
}
