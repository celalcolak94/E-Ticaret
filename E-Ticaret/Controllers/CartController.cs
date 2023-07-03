using Microsoft.AspNetCore.Mvc;
using E_Ticaret.Extensions;
using E_Ticaret.Data;
using E_Ticaret.Models;
using E_Ticaret.Data.Entities;

namespace E_Ticaret.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private DataContext db = new DataContext();

        public IActionResult Index()
        {
            var cart = GetCart();

            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = db.Products.Find(id);

            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product,1);
                _httpContextAccessor?.HttpContext?.Session.SetCart("Cart", cart);
            }

            return RedirectToAction("Index");

        }

        public IActionResult RemoveFromCart(int id)
        {
            var product = db.Products.Find(id);

            if (product != null)
            {
                var cart = GetCart();
                cart.DeleteProduct(product);
                _httpContextAccessor?.HttpContext?.Session.SetCart("Cart", cart);
            }

            return RedirectToAction("Index");

        }

        public Cart GetCart()
        {
            var cart = _httpContextAccessor?.HttpContext?.Session.GetCart("Cart");

            return cart;
        }

        public PartialViewResult Summary()
        {
            var cart = GetCart();


            return PartialView("Summary",cart);
        }
    }
}
