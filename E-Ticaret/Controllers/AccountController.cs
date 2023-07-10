using E_Ticaret.Data;
using E_Ticaret.Data.Entities;
using E_Ticaret.Migrations;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Ticaret.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();

        [Authorize(AuthenticationSchemes = "CLL94")]
        public IActionResult Index()
        {
            var orders = db.Orders
                .Where(x => x.UserName == User.Identity.Name)
                .Select(x => new UserOrderModel()
                {
                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    OrderDate = x.OrderDate,
                    OrderState = (Data.Entities.EnumOrderState)x.OrderState,
                    TotalPrice = x.TotalPrice
                })
                .OrderByDescending(x => x.OrderDate).ToList();

            return View(orders);
        }

        [Authorize(AuthenticationSchemes = "CLL94")]
        public IActionResult Details(int id)
        {
            var entity = db.Orders.Where(x => x.Id == id)
                .Select(x => new OrderDetailsModel()
                {
                    OrderId = x.Id,
                    OrderNumber = x.OrderNumber,
                    TotalPrice = x.TotalPrice,
                    OrderDate = x.OrderDate,
                    OrderState = x.OrderState,
                    AdresBasligi = x.AdresBasligi,
                    Adres = x.Adres,
                    Sehir = x.Sehir,
                    Semt = x.Semt,
                    Mahalle = x.Mahalle,
                    PostaKodu = x.PostaKodu,
                    OrderLines = x.OrderLines.Select(y => new OrderLineModel()
                    {
                        ProductId = y.ProductId,
                        ProductName = y.Product.Name.Length > 50 ? y.Product.Name.Substring(0,47) + "..." : y.Product.Name,
                        Image = y.Product.Image,
                        Quantity = y.Quantity,
                        Price = y.Price
                    }).ToList()
                }).FirstOrDefault();

            return View(entity);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var user = db.Users.Include(x => x.Roles).FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

                if (user != null)
                {
                    List<Claim> claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));

                    if (user.Roles != null)
                    {
                        foreach (var role in user.Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                        }
                    }


                    var claimPrinciple = new ClaimsPrincipal(new ClaimsIdentity(claims, "CLL94"));

                    var authProps = new AuthenticationProperties();
                    authProps.IsPersistent = model.RememberMe;

                    if (model.RememberMe)
                    {
                        authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30);
                    }

                    HttpContext.SignInAsync(claimPrinciple, authProps);

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("LoginError", "Böyle bir kullanıcı yok.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "CLL94")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync("CLL94");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User();
                    user.Name = model.Name;
                    user.SurName = model.SurName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.Password = model.Password;

                    db.Users.Add(user);
                    db.SaveChanges();
                    ModelState.Clear();

                    ViewBag.Message = "Kayıt başarılı, yönlendiriliyorsunuz";
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Email", "Aynı E-posta adresinden mevcut");
                    ModelState.AddModelError("UserName", "Aynı Username mevcut");
                }

                return View();

            }
            else
            {
                return View(model);
            }
        }
    }
}
