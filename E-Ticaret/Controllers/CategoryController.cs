using E_Ticaret.Data;
using E_Ticaret.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class CategoryController : Controller
    {
        DataContext db = new DataContext();

        public IActionResult Index()
        {
            var categories = db.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var category = db.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
