using Microsoft.AspNetCore.Mvc;
using E_Ticaret.Extensions;
using E_Ticaret.Data;
using E_Ticaret.Models;
using E_Ticaret.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace E_Ticaret.Controllers
{
    [Authorize(AuthenticationSchemes = "CLL94")]
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
                if (product.Stock > 0)
                {
                    var cart = GetCart();
                    cart.AddProduct(product, 1);
                    _httpContextAccessor?.HttpContext?.Session.SetCart("Cart", cart);
                }

            }

            return RedirectToAction("Index");

        }

        public IActionResult ReduceToCart(int id)
        {
            var product = db.Products.Find(id);

            if (product != null)
            {
                if (product.Stock > 0)
                {
                    var cart = GetCart();
                    cart.ReduceProduct(product);
                    _httpContextAccessor?.HttpContext?.Session.SetCart("Cart", cart);
                }

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

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "CLL94")]
        public IActionResult Checkout(ShippingDetails shipping)
        {
            var cart = GetCart();

            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    SaveOrder(cart,shipping);

                    cart.Clear();
                    _httpContextAccessor?.HttpContext?.Session.SetCart("Cart", cart);
                    return View("Completed");

                }
                else
                {
                    return View(shipping);
                }
            }


            return View();
        }

        [Authorize(AuthenticationSchemes = "CLL94")]
        private void SaveOrder(Cart cart, ShippingDetails shipping)
        {
            var order = new Order();

            order.OrderNumber = "A" + new Random().Next(10000, 99999).ToString();
            order.TotalPrice = cart.Total();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            order.UserName = shipping.UserName;
            order.AdresBasligi = shipping.AdresBasligi;
            order.Adres = shipping.Adres;
            order.Sehir = shipping.Sehir;
            order.Semt = shipping.Semt;
            order.Mahalle = shipping.Mahalle;
            order.PostaKodu = shipping.PostaKodu;
            order.OrderLines = new List<OrderLine>();

            foreach (var product in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = product.Quantity;
                orderline.Price = product.Quantity * product.Product.Price;
                orderline.ProductId = product.Product.Id;

                order.OrderLines.Add(orderline);
            }

            //Stoktan düşme işlemi
            foreach (var item in cart.CartLines)
            {
                var p = db.Products.FirstOrDefault(x => x.Id == item.Product.Id);
                p.Stock -= item.Quantity;

                db.Products.Update(p);

            }

            db.Orders.Add(order);
            db.SaveChanges();

        }
    }
}
