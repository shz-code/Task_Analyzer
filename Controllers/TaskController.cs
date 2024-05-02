using Microsoft.AspNetCore.Mvc;

namespace Task_Analyzer.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Name"] = "Shanto";
            return View();
        }
    }
}
