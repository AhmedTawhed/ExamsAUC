using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;
using MVCExamProject.ViewModel;
using System.Linq;
using MVCExamProject.Enums;
using MVCExamProject.Repository;

namespace MVCExamProject.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class ExamController : Controller
    {
        private readonly IExamRepository ExamRepo;
        private readonly IExamQuestionRepository QuestionRepo;
        private readonly IQuestionOptionRepository OptionRepo;

        public ExamController(
            IExamRepository _examRepo,
            IExamQuestionRepository questionRepo,
            IQuestionOptionRepository optionRepo
        )
        {
            this.ExamRepo = _examRepo;
            QuestionRepo = questionRepo;
            OptionRepo = optionRepo;

        }

        [Route("admin/exams")]
        public IActionResult Index()
        {
            var exams = ExamRepo.GetAll();
            string responseData = TempData["ResponseData"] as string;
            if (!string.IsNullOrEmpty(responseData))
            {
                ViewData["Response"] = responseData;
            }
            return View("~/Views/Admin/Exam/index.cshtml" , exams);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/exams/create")]
        public IActionResult Create(ExamViewModel data)
        {
            if (ModelState.IsValid)
            {
                Exam exam = new Exam() { Name = data.Title, QuestionCount = int.Parse(data.QuestionsCount) };
                ExamRepo.Insert(exam);

                for (int i = 1; i <= int.Parse(data.QuestionsCount); i++)
                {
                    string questionTitle = Request.Form["Questions[" + i + "].Title"];
                    string questionType = Request.Form["Questions[" + i + "].QuestionType"];

                    ExamQuestion question = new ExamQuestion()
                    {
                        Title = questionTitle,
                        ExamId = exam.Id,
                        QuestionType = questionType
                    };
                    QuestionRepo.Insert(question);

                    if (questionType == "MCQ")
                    {
                        for (int j = 1; j <= 4; j++)
                        {
                            string optionTitle = Request.Form["Questions[" + i + "].Options[" + (j - 1) + "].Text"];
                            bool isRight = (Request.Form["Questions[" + i + "].CorrectOption"] == j.ToString());

                            QuestionOption option = new QuestionOption()
                            {
                                Title = optionTitle,
                                IsRight = isRight,
                                ExamQuestionId = question.Id
                            };
                            OptionRepo.Insert(option);
                        }
                    }
                    else if (questionType == "TrueFalse")
                    {
                        string optionTitleTrue = "True";
                        string optionTitleFalse = "False";

                        bool isRightTrue = (Request.Form["Questions[" + i + "].CorrectOption"] == "true");
                        bool isRightFalse = !isRightTrue;

                        QuestionOption optionTrue = new QuestionOption()
                        {
                            Title = optionTitleTrue,
                            IsRight = isRightTrue,
                            ExamQuestionId = question.Id
                        };
                        OptionRepo.Insert(optionTrue);

                        QuestionOption optionFalse = new QuestionOption()
                        {
                            Title = optionTitleFalse,
                            IsRight = isRightFalse,
                            ExamQuestionId = question.Id
                        };
                        OptionRepo.Insert(optionFalse);
                    }
                
                    else if (questionType == "Essay" || questionType == "MissingWord")
                    {
                        string optionTitle = Request.Form["Questions[" + i + "].Answer"];
                        bool isRight = true; 

                        QuestionOption option = new QuestionOption()
                        {
                            Title = optionTitle,
                            IsRight = isRight,
                            ExamQuestionId = question.Id
                        };
                        OptionRepo.Insert(option);
                    }
                }

                OptionRepo.SaveChanges();
                TempData["ResponseData"] = Responses.success.ToString();
            }
            else
            {
                TempData["ResponseData"] = Responses.fail.ToString();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("admin/exams/create")]
        public IActionResult Create()
        {

            return View("~/Views/Admin/Exam/create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ExamViewModel data, int id)
        {
            try
            {
                Exam exam = ExamRepo.GetById(id);
                ExamRepo.Delete(exam);
                TempData["ResponseData"] = Responses.success.ToString();
               
            }
            catch(Exception e)
            {
                TempData["ResponseData"] = Responses.fail.ToString();
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/exams/{id}/edit")]
        public IActionResult Edit(Exam exam)
        {
            var request = Request.Form;
            ExamRepo.Update(exam , request);
            TempData["ResponseData"] = Responses.success.ToString();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("admin/exams/{id}/edit")]
        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                Exam exam = ExamRepo.getExam(id);

                return View("~/Views/Admin/Exam/edit.cshtml" , exam);
            }
            return RedirectToActionPermanent("Index");
        }

    }
}
