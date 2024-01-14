using ExpensesWebApp.Data;
using ExpensesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesWebApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly ExpensesAppDbContext _db;

        public CategoryController(ExpensesAppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Category> categories = await _db.Categories.ToListAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create(string? returnTo)
        {
            ViewData["returnTo"] = returnTo;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category, string? returnTo)
        {

            bool exists = _db.Categories.Any(c => c.Name == category.Name);

            if (exists)
            {
                ViewData["returnTo"] = returnTo;
                ModelState.AddModelError("Name", "Essa categoria já existe.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChangesAsync();
                TempData["success"] = "Categoria criada com sucesso.";

                if (returnTo == null)
                {
                    return RedirectToAction("Index");
                }

                if (returnTo.Contains("edit"))
                {
                    var id = returnTo.Remove(0, "edit/".Length);
                    return RedirectToAction("Edit", "Expense", new { id = id });
                }

                return RedirectToAction("Create", "Expense", new { groupId = returnTo });

            }

            TempData["error"] = "Algum erro ocorreu.";

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = await _db.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            bool exists = await _db.Categories.AnyAsync(c => c.Name == category.Name && c.Id != category.Id);

            if (exists)
            {
                ModelState.AddModelError("Name", "Essa categoria já existe.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Categoria atualizada com sucesso.";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = await _db.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            _db.Remove(category);
            await _db.SaveChangesAsync();
            TempData["success"] = "Categoria excluída com sucesso.";

            return RedirectToAction("Index");
        }

    }
}
