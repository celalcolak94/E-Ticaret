using E_Ticaret.Data;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Diagnostics;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        public IActionResult Index()
        {
            var urunler = db.Products
                .Where(x => x.IsHome == true && x.IsApproved == true)
                .Select(x => new ProductModel()
                {
                    Id = x.Id,
                    Name = x.Name.Length > 50 ? x.Name.Substring(0, 47) + "..." : x.Name,
                    Description = x.Description.Length > 50 ? x.Description.Substring(0,47) + "..." : x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    Image = x.Image == null ? "holder.png" : x.Image,
                    CategoryId = x.CategoryId
                }).ToList();

            return View(urunler);
        }

        [HttpGet("urun-detay", Name = "details")]
        public IActionResult Details(int id)
        {
            var urun = db.Products.Find(id);

            return View(urun);
        }

        [HttpGet("urun-listesi", Name = "list")]
        public IActionResult List(int? id)
        {
            var urunler = db.Products
                .Where(x => x.IsApproved == true)
                .Select(x => new ProductModel()
                {
                    Id = x.Id,
                    Name = x.Name.Length > 50 ? x.Name.Substring(0, 47) + "..." : x.Name,
                    Description = x.Description.Length > 50 ? x.Description.Substring(0, 47) + "..." : x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    Image = x.Image == null ? "holder.png" : x.Image,
                    CategoryId = x.CategoryId
                })
                .ToList();

            if (id != null)
            {
                urunler = urunler.Where(x => x.CategoryId == id).ToList();
            }

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.CategoryId = id;

            return View(urunler);
        }

    }
}