using E_Ticaret.Data;
using E_Ticaret.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    [Authorize(AuthenticationSchemes = "CLL94",Roles = "Admin")]
    public class CategoryController : Controller
    {
        DataContext db = new DataContext();

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Index()
        {
            var categories = db.Categories.ToList();

            return View(categories);
        }

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var category = db.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Update(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Delete(Category category)
        {
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
