using E_Ticaret.Data;
using E_Ticaret.Data.Entities;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Ticaret.Controllers
{
    public class OrderController : Controller
    {
        DataContext db = new DataContext();

        [Authorize(AuthenticationSchemes = "CLL94", Roles = "Admin")]
        public IActionResult Index()
        {
            var orders = db.Orders.Select(x => new AdminOrderModel()
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                OrderDate = x.OrderDate,
                OrderState = (Data.Entities.EnumOrderState)x.OrderState,
                TotalPrice = x.TotalPrice,
                Count = x.OrderLines.Count
            }).OrderByDescending(x => x.OrderDate).ToList();

            return View(orders);
        }

        public IActionResult Details(int id, int OrderId, EnumOrderState OrderState)
        {
            //OrderState değişiklik yapılacak order
            var order = db.Orders.FirstOrDefault(x => x.Id == OrderId);

            if (order != null)
            {
                order.OrderState = OrderState;
                db.Orders.Update(order);
                db.SaveChanges();

                ViewBag.Message = "Başarı ile kayıt edildi.";

            }


            var entity = db.Orders.Where(x => x.Id == id)
               .Select(x => new OrderDetailsModel()
               {
                   OrderId = x.Id,
                   UserName = x.UserName,
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
                       ProductName = y.Product.Name.Length > 50 ? y.Product.Name.Substring(0, 47) + "..." : y.Product.Name,
                       Image = y.Product.Image,
                       Quantity = y.Quantity,
                       Price = y.Price
                   }).ToList()
               }).FirstOrDefault();

            ViewBag.EnumOrderStates = Enum.GetValues(typeof(EnumOrderState))
                                       .Cast<EnumOrderState>()
                                       .ToList();


            return View(entity);
        }

        //public IActionResult UpdateOrderState(int OrderId, EnumOrderState OrderState)
        //{
        //    var order = db.Orders.FirstOrDefault(x => x.Id == OrderId);

        //    if (order != null)
        //    {
        //        order.OrderState = OrderState;
        //        db.Orders.Update(order);
        //        db.SaveChanges();

        //        ViewBag.Message = "Başarı ile kayıt edildi.";

        //        return RedirectToAction("Details", new { id = OrderId });
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}
