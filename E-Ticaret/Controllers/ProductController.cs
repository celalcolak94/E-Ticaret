using E_Ticaret.Data;
using E_Ticaret.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
    public class ProductController : Controller
    {
        DataContext db = new DataContext();

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Index()
        {
            var products = db.Products.Include(x => x.Category).ToList();

            return View(products);
        }

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();

            return View();
        }

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
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

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var product = db.Products.Find(id);

            ViewBag.Categories = db.Categories.ToList();

            return View(product);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Update(Product product)
        {
            db.Products.Update(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public  IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

            return View(product);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Delete(Product product)
        {
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index","Product");
        }
    }
}
