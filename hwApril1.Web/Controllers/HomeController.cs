using hwApril1.Data;
using hwApril1.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hwApril1.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new QuestionRepository(_connectionString);
            var vm = new HomeViewModel()
            {
                Questions = repo.GetQuestions()
            };
            return View(vm);
        }
    }
}
