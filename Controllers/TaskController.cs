using Microsoft.AspNetCore.Mvc;
using Task_Analyzer.Data;
using Task_Analyzer.Models;

namespace Task_Analyzer.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDBContext _db;

        public TaskController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Models.Task model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(DateTime.Now > model.DueDate)
            {
                TempData["Error"] = "Task due date can not be set to past!";
                return View(model);
            }

            _db.Add(model);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Task created successfully!";
            return RedirectToAction("Create");
        }
    }
}
