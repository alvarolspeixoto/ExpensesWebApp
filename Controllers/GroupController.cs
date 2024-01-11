using ExpensesWebApp.Data;
using ExpensesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesWebApp.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {

        private readonly ExpensesAppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupController(ExpensesAppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<Group> groups = _db.Groups.Where(g => g.UserId == userId);

            return View(groups);
        }

        public async Task<IActionResult> Details(int? id)
        {

            var group = await _db.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (group.UserId != userId)
            {
                return NotFound();
            }

            string groupName = group.Name!;

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
        public async Task<IActionResult> Create(Group group)
        {

            bool exists = await _db.Groups.AnyAsync(e => e.Name == group.Name);

            if (exists)
            {
                ModelState.AddModelError("name", "Já existe um grupo com esse nome.");
            } 

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                group.UserId = userId;

                _db.Groups.Add(group);
                await _db.SaveChangesAsync();
                TempData["success"] = "Grupo de despesas criado com sucesso.";

                return RedirectToAction("Index");
            }

            TempData["error"] = "Algum erro ocorreu.";
            return View(group);
        }

        // GET
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var group = await _db.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var group = await _db.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            _db.Groups.Remove(group);
            await _db.SaveChangesAsync();
            TempData["success"] = "Grupo excluído com sucesso.";
            return RedirectToAction("Index");
        }
    }
}
