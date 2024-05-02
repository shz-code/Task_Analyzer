using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Models.Task> tasks = await _db.Tasks.OrderBy(t => t.Priority).ToListAsync();

            int total = tasks.Count();
            int completed = 0;
            int pending = 0;
            int overdue = 0;

            List<Models.Task> seriousTasks = new List<Models.Task>();

            foreach(Models.Task t in tasks)
            {
                TimeSpan difference = t.DueDate - DateTime.Today;
                int dayGap = (int)difference.TotalDays;

                if(dayGap <= 1 && !t.IsCompleted) seriousTasks.Add(t);

                if (t.IsCompleted) completed++;
                else if(!t.IsCompleted && DateTime.Now > t.DueDate) overdue++;
                else if(!t.IsCompleted) pending++;
            }

            ViewBag.total = total;
            ViewBag.completed = completed;
            ViewBag.pending = pending;
            ViewBag.overdue = overdue;
            ViewBag.seriousTasks = seriousTasks;

            return View(tasks);
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
        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var task = await _db.Tasks.FindAsync(id);

            if (task == null)
            {
                return RedirectToAction("Index");
            }

            task.IsCompleted = true;
            _db.Update(task);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
