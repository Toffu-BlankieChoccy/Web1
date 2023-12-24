using Microsoft.AspNetCore.Mvc;
using Project_63135901.Extensions;
using Project_63135901.ModelViews;

namespace Project_63135901.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            
            return View(cart);
        }
    }
}
