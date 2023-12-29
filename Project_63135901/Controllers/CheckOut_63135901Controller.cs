using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_63135901.Extensions;
using Project_63135901.Helper;
using Project_63135901.Models;
using Project_63135901.ModelViews;

namespace Project_63135901.Controllers
{
    public class CheckOut_63135901Controller : Controller
    {
        private readonly PROJECT_63135901Context _context;
        public INotyfService _notyfService { get; }
        public CheckOut_63135901Controller(PROJECT_63135901Context context, INotyfService notifyfService)
        {
            _context = context;
            _notyfService = notifyfService;

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

        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(string returnUrl = null)
        {
            // lấy giỏ hàng ra để xử lý
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("CustomersId");
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomersId == Convert.ToInt32(taikhoanID));
                model.CustomerId = khachhang.CustomersId;
                model.FullName = khachhang.FullName;
                model.Email = khachhang.Email;
                model.Phone = khachhang.Phone;
                model.Address = khachhang.CusAddress;
            }
            ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.NameWithType).ToList(), "LocationId", "LocationName");
            ViewBag.GioHang = cart;
            return View(model);
        }

        [HttpPost]
        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(MuaHangVM muaHang)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanID = HttpContext.Session.GetString("CustomersId");
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomersId == Convert.ToInt32(taikhoanID));
                model.CustomerId = khachhang.CustomersId;
                model.FullName = khachhang.FullName;
                model.Email = khachhang.Email;
                model.Phone = khachhang.Phone;
                model.Address = khachhang.CusAddress;

                khachhang.LocationId = muaHang.TinhThanh;
                khachhang.District = muaHang.QuanHuyen;
                khachhang.Ward = muaHang.PhuongXa;
                khachhang.CusAddress = muaHang.Address;

                model.TinhThanh = Convert.ToInt32(khachhang.LocationId);
                model.QuanHuyen = khachhang.District;
                model.PhuongXa = khachhang.Ward;

                _context.Update(khachhang);
                _context.SaveChanges();
            }
            try
            {
                    // khởi tạo đơn hàng
                    Order donhang = new Order();
                    donhang.CustomersId = model.CustomerId;
                    donhang.CusAddress = model.Address;
                    donhang.LocationId = model.TinhThanh;
                    donhang.District = model.QuanHuyen;
                    donhang.Ward = model.PhuongXa;

                    donhang.OrderDate = DateTime.Now;
                    donhang.TransactStatusId = 1;
                    donhang.Deleted = false;
                    donhang.Paid = false;
                    donhang.Note = Utilities.StripHTML(model.Note);
                    donhang.TotalMoney = Convert.ToInt32(cart.Sum(x => x.TotalMoney));
                    _context.Add(donhang);
                    _context.SaveChanges();

                    // tạo mới ds đơn hàng
                    foreach (var item in cart)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = donhang.OrderId;
                        orderDetail.ProductId = item.product.ProductId;
                        orderDetail.Quantity = item.amount;
                        orderDetail.Total = donhang.TotalMoney;
                        orderDetail.Price = item.product.Price;
                        orderDetail.ShipDate = DateTime.Now;
                        _context.Add(orderDetail);
                        /* _context.OrderDetails.Attach(orderDetail);
                         _context.Entry(orderDetail).State = EntityState.Modified;*/
                    }

                    _context.SaveChanges();
                    // clear giỏ hàng
                    HttpContext.Session.Remove("GioHang");

                    _notyfService.Success("Đặt hàng thành công");

                    return RedirectToAction("Success");

            }
            catch (Exception ex)
            {
                ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.NameWithType).ToList(), "LocationId", "LocationName");
                ViewBag.GioHang = cart;
                return View(model);
            }
            ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.NameWithType).ToList(), "LocationId", "LocationName");
            ViewBag.GioHang = cart;
            return View(model);
        }




        [Route("dat-hang-thanh-cong.html", Name = "Success")]
        public IActionResult Success()
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("CustomersId");
                if (string.IsNullOrEmpty(taikhoanID))
                {
                    return RedirectToAction("Login", "Accounts_63135901", new { returnUrl = "/dat-hang-thanh-cong.html" });
                }
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomersId == Convert.ToInt32(taikhoanID));
                var donhang = _context.Orders.Where(x => x.CustomersId == Convert.ToInt32(taikhoanID)).OrderByDescending(x => x.OrderDate).FirstOrDefault();
                MuaHangSuccessVM successVM = new MuaHangSuccessVM();
                successVM.FullName = khachhang.FullName;
                successVM.DonHangID = donhang.OrderId;
                successVM.Phone = khachhang.Phone;
                successVM.Address = khachhang.CusAddress;
                successVM.PhuongXa = donhang.Ward;
                successVM.TinhThanh = donhang.District;
                return View(successVM);
            }
            catch
            {
                return View();
            }
        }



        public string GetNameLocation(int idlocation)
        {
            try
            {
                var location = _context.Locations.AsNoTracking().SingleOrDefault(x => x.LocationId == idlocation);
                if (location != null)
                {
                    return location.NameWithType;
                }
            }
            catch
            {
                return string.Empty;
            }
            return string.Empty;
        }
    }
}
