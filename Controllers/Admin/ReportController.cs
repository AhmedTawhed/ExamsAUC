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
    public class ReportController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IUserExamRepository userExamRepository;
        private readonly IExamRepository examRepository;

        public ReportController(IUserRepository userRepository,IUserExamRepository userExamRepository,IExamRepository examRepository)
        {
            this.userRepository = userRepository;
            this.userExamRepository = userExamRepository;
            this.examRepository = examRepository;
        }



        [Route("admin/reports")]
        public IActionResult Index()
        {
            List<UserExam> userExams = userExamRepository.GetAll().ToList();
            foreach (var item in userExams)
            {
                item.Exam = examRepository.GetById(item.ExamId);
                item.User = userRepository.GetById(item.UserId);
            }
            return View("~/Views/Admin/Report/index.cshtml", userExams);
        }

       
        public IActionResult searchName(string name) { 
            var result = userExamRepository.getExamByUserName(name).ToList();
            foreach (var item in result)
            {
                item.Exam = examRepository.GetById(item.ExamId);
                item.User = userRepository.GetById(item.UserId);
            }
            return View("~/Views/Admin/Report/index.cshtml", result);
        }



    }
}
