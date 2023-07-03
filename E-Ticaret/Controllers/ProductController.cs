using E_Ticaret.Data;
using E_Ticaret.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class ProductController : Controller
    {
        DataContext db = new DataContext();

        public IActionResult Index()
        {
            var products = db.Products.Include(x => x.Category).ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product.Image == null)
            {
                product.Image = "holder.png";
            }

            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var product = db.Products.Find(id);

            ViewBag.Categories = db.Categories.ToList();

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            db.Products.Update(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public  IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index","Product");
        }
    }
}
