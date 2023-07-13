using E_Ticaret.Data;
using E_Ticaret.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.ViewComponents
{
    public class CartSummary : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartSummary(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var cart = _httpContextAccessor?.HttpContext?.Session.GetCart("Cart");

            return View(cart);
        }
    }
}
