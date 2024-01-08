using ExpensesWebApp.Data;
using ExpensesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesWebApp.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {

        private readonly ExpensesAppDbContext _db;

        public GroupController(ExpensesAppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Group> groups = _db.Groups.ToList();

            return View(groups);
        }

        public IActionResult Details(int? id)
        {

            var group = _db.Groups.Find(id);

            if (group == null)
            {
                return NotFound();
            }

            string groupName = group.Name;

            IEnumerable<Expense> groupExpenses = _db.Expenses.Where(a => a.GroupId == id).Include(e => e.Category);

            ViewData["groupName"] = groupName;
            ViewData["groupId"] = id;
            return View(groupExpenses);
        }

        // GET
        public IActionResult Create()
        {

            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Group group)
        {

            bool exists = _db.Groups.Any(e => e.Name == group.Name);

            if (exists)
            {
                ModelState.AddModelError("name", "Já existe um grupo com esse nome.");
            } 

            if (ModelState.IsValid)
            {
                _db.Groups.Add(group);
                _db.SaveChanges();
                TempData["success"] = "Grupo de despesas criado com sucesso";

                return RedirectToAction("Index");
            }

            TempData["error"] = "Algum erro ocorreu";
            return View(group);
        }

        // GET
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var group = _db.Groups.Find(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var group = _db.Groups.Find(id);

            if (group == null)
            {
                return NotFound();
            }

            _db.Groups.Remove(group);
            _db.SaveChanges();
            TempData["success"] = "Grupo excluído com sucesso";
            return RedirectToAction("Index");
        }
    }
}
