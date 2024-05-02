using Microsoft.AspNetCore.Mvc;

namespace Task_Analyzer.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
