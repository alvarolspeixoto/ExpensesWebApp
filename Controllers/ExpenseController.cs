using ExpensesWebApp.Data;
using ExpensesWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesWebApp.Controllers
{
    public class ExpenseController : Controller
    {

        private readonly ExpensesAppDbContext _db;
        public ExpenseController(ExpensesAppDbContext db)
        {
            _db = db;
        }

        // GET
        public IActionResult Create(int? groupId)
        {
            if (groupId == null || groupId == 0)
            {
                return NotFound();
            }

            var group = _db.Groups.Find(groupId);

            if (group == null)
            {
                return NotFound();
            }

            string groupName = group.Name;
            IEnumerable<Category> categories = _db.Categories.ToList();
            ViewBag.Categories = categories;
            ViewData["groupId"] = groupId;
            ViewData["groupName"] = groupName;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Expense expense)
        {

            if (ModelState.IsValid)
            {
                _db.Expenses.Add(expense);
                _db.SaveChanges();
                return RedirectToAction("Details", "Group", new {id = expense.GroupId});
            }

            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var expense = _db.Expenses.Find(id);

            if (expense == null)
            {
                return NotFound();
            }

            _db.Expenses.Remove(expense);
            _db.SaveChanges();
            return RedirectToAction("Details", "Group", new { id = expense.GroupId });
        }


    }
}
