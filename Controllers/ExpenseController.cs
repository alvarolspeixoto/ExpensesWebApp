using ExpensesWebApp.Data;
using ExpensesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesWebApp.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {

        private readonly ExpensesAppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExpenseController(ExpensesAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // GET
        public async Task<IActionResult> Create(int? groupId)
        {
            if (groupId == null || groupId == 0)
            {
                return NotFound();
            }

            var group = await _db.Groups.FindAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (group.UserId != userId)
            {
                return NotFound();
            }

            IEnumerable<Category> categories = await _db.Categories.ToListAsync();
            ViewBag.Categories = categories;
            ViewData["groupId"] = groupId;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {

            DateTime currentDate = DateTime.Now;
            var expenseDate = expense.Date;

            if (expenseDate > currentDate)
            {
                ModelState.AddModelError("Date", "A data inserida é posterior à data atual.");
            }

            if (ModelState.IsValid)
            {
                
                expense.Date = expenseDate.ToUniversalTime();
                _db.Expenses.Add(expense);
                await _db.SaveChangesAsync();
                TempData["success"] = "Despesa adicionada com sucesso.";
                return RedirectToAction("Details", "Group", new {id = expense.GroupId});
            }

            ViewData["groupId"] = expense.GroupId;
            IEnumerable<Category> categories = await _db.Categories.ToListAsync();
            ViewBag.Categories = categories;
            TempData["error"] = "Algum erro ocorreu.";

            return View(expense);
        }

        public async Task<IActionResult> Edit(int? id) {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var expense = await _db.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            var groupId = expense.GroupId;
            var group = await _db.Groups.FindAsync(groupId);
            var userId = _userManager.GetUserId(User);

            if (group!.UserId != userId)
            {
                return NotFound();
            }

            IEnumerable<Category> categories = await _db.Categories.ToListAsync();
            ViewBag.Categories = categories;
            ViewData["groupId"] = groupId;
            ViewData["id"] = id;

            return View(expense);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Expense expense)
        {
            DateTime currentDate = DateTime.Now;
            var expenseDate = expense.Date;

            if (expenseDate > currentDate)
            {
                ModelState.AddModelError("Date", "A data inserida é posterior à data atual.");
            }

            if (ModelState.IsValid)
            {
                expense.Date = expenseDate.ToUniversalTime();
                _db.Expenses.Update(expense);
                await _db.SaveChangesAsync();
                TempData["success"] = "Despesa editada com sucesso.";
                return RedirectToAction("Details", "Group", new {id = expense.GroupId});

            }

            ViewData["groupId"] = expense.GroupId;
            IEnumerable<Category> categories = await _db.Categories.ToListAsync();
            ViewBag.Categories = categories;
            TempData["error"] = "Algum erro ocorreu.";

            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var expense = await _db.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            _db.Expenses.Remove(expense);
            await _db.SaveChangesAsync();
            TempData["success"] = "Despesa excluída com sucesso.";
            return RedirectToAction("Details", "Group", new { id = expense.GroupId });
        }


    }
}
