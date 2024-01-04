using ExpensesWebApp.Data;
using ExpensesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExpensesWebApp.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ExpensesAppDbContext _db;

        public CategoryController(ExpensesAppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            IEnumerable<Category> categories = _db.Categories.ToList();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {

            bool exists = _db.Categories.Any(c => c.Name == category.Name);

            if (exists)
            {
                ModelState.AddModelError("Name", "Essa categoria já existe");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Categoria criada com sucesso.";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            bool exists = _db.Categories.Any(c => c.Name == category.Name && c.Id != category.Id);

            if (exists)
            {
                ModelState.AddModelError("Name", "Essa categoria já existe.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Categoria atualizada com sucesso.";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            _db.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Categoria excluída com sucesso";

            return RedirectToAction("Index");
        }

    }
}
