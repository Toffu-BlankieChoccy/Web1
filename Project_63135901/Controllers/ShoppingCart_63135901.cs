using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_63135901.Extensions;
using Project_63135901.Models;
using Project_63135901.ModelViews;

namespace Project_63135901.Controllers
{
    public class ShoppingCart_63135901Controller : Controller
    {
        private readonly PROJECT_63135901Context _context;
        public INotyfService _notyfService { get; }

        public ShoppingCart_63135901Controller(PROJECT_63135901Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }


        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }


        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID, int? quantity)
        {
            List<CartItem> gioHang = GioHang;
            // thêm sản phảm vào giỏ hàng
            try
            {
                CartItem item = gioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    if (quantity.HasValue)
                    {
                        item.amount = quantity.Value;
                    }
                    else
                    {
                        item.amount++;
                    }
                }
                else
                {
                    Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                    item = new CartItem
                    {
                        amount = quantity.HasValue ? quantity.Value : 1,
                        product = hh
                    };
                    gioHang.Add(item); // thêm vào giỏ
                }
                // lưu lại session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                _notyfService.Success("Sản phẩm đã được thêm vào giỏ hàng");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }


        [HttpPost]
        [Route("api/cart/remove")]
        public IActionResult Remove(int productID)
        {
            try
            {
                List<CartItem> gioHang = GioHang;
                CartItem item = gioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                // lưu lại session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }



        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
               return View(GioHang);          
        }
    }
}
