using hwApril1.Data;
using hwApril1.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hwApril1.Web.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {

        private string _connectionString;
        public QuestionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult AskAQuestion()
        {
            return View();
        }

        public IActionResult Add(Question question, List<string> tags)
        {
            var Qrepo = new QuestionRepository(_connectionString);
            var userRepo = new UserRepository(_connectionString);
            question.UserId = userRepo.GetByEmail(User.Identity.Name).Id;
            question.Date = DateTime.Today;
            Qrepo.AddQuestion(question, tags);
            return Redirect($"/question/viewquestion?id={question.Id}");
        }

        [AllowAnonymous]
        public IActionResult ViewQuestion(int id)
        {
            var repo = new QuestionRepository(_connectionString);
            var vm = new QuestionViewModel()
            {
                Question = repo.GetCompleteQuestionById(id)
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddAnswer(Answer answer)
        {
            answer.Date = DateTime.Today;
            var userRepo = new UserRepository(_connectionString);
            answer.UserId = userRepo.GetByEmail(User.Identity.Name).Id;
            var answerRepo = new AnswerRepository(_connectionString);
            answerRepo.AddAnswer(answer);
            return Redirect($"/questions/viewquestion?id={answer.QuestionId}");
        }

        [AllowAnonymous]
        public IActionResult ViewForTag(string tagName)
        {
            var repo = new TagRepository(_connectionString);
            var vm = new TagViewModel()
            {
                Questions = repo.GetQuestionsForTag(tagName)
            };
            return View(vm);
        }
    }
}
