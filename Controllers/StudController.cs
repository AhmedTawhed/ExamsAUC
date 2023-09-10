using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCExamProject.Models;
using MVCExamProject.Repository;
using MVCExamProject.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MVCExamProject.Controllers
{
	public class StudController : Controller
	{
		private readonly IExamRepository examService;
        private readonly IExamQuestionRepository examQuestionService;
        private readonly IQuestionOptionRepository questionOptionService;
        private readonly IUserExamRepository userExamService;

        public StudController(IExamRepository examService,IExamQuestionRepository examQuestionService,IQuestionOptionRepository questionOptionService,IUserExamRepository userExamService)
        {
			this.examService = examService;
            this.examQuestionService = examQuestionService;
            this.questionOptionService = questionOptionService;
            this.userExamService = userExamService;
        }

	

		public IActionResult ChooseExam()
		{
			var exams = examService.GetAll();
			return View(exams);
		}
		public IActionResult ExamQuestions(int Id)
		{
            var questionsAndOptions = examService.getExam(Id).ExamQuestions.ToList();
            ViewBag.examId = Id;
            
            return View(questionsAndOptions);
        }
        [HttpPost]
        public IActionResult Result(List<int> optionsId,List<string> answers,int examId)
        {
            
           
            int count = 0;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserExam userExam = new UserExam();
            userExam.ExamId = examId;
            userExam.Exam = examService.GetById(examId);
            userExam.UserId = int.Parse(userId);

            foreach (var answer in answers)
            {
                var answerId = questionOptionService.GetIdByTitle(answer);
                optionsId.Add(answerId);
            }

            foreach (int id in optionsId)
            {
                var option = questionOptionService.GetById(id);
                if (option != null && option.IsRight == true)
                {
                    count++;
                }      
            }

            if (count > userExam.Exam.QuestionCount/2)
            {
                userExam.IsPassed = true;

            } else { userExam.IsPassed = false; }

            userExam.Degree = count;
            userExam.CreatedAt = DateTime.Now;
            userExamService.Insert(userExam);

            return View(userExam);
        }

    }
}
