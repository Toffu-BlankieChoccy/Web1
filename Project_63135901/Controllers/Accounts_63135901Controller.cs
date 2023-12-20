using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_63135901.Extensions;
using Project_63135901.Helper;
using Project_63135901.Models;
using Project_63135901.ModelViews;
using System.Security.Claims;

namespace Project_63135901.Controllers
{
	[Authorize]
	public class Accounts_63135901Controller : Controller
	{
		private readonly PROJECT_63135901Context _context;
		public INotyfService _notyfService { get; }

		public Accounts_63135901Controller(PROJECT_63135901Context context, INotyfService notyfService)
		{
			_context = context;
			_notyfService = notyfService;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult ValidatePhone(string Phone)
		{
			try
			{
				var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower()); 
				if (khachhang != null)
					return Json(data: "Số điện thoại : " + Phone + "Đã được sử dụng ");
				return Json(data: true);
			}
			catch
			{
				return Json(data: true);
			}
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult ValidateEmail(string Email)
		{
			try
			{
				var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
				if (khachhang != null)
					return Json(data: "Email : " + Email + "Đã được sử dụng ");
				return Json(data: true);
			}
			catch
			{
				return Json(data: true);
			}
		}

        [Authorize]
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
		{
			var taikhoanID = HttpContext.Session.GetString("CustomersId");
			if (taikhoanID != null)
			{
				var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomersId == Convert.ToInt32(taikhoanID));
				if(khachhang != null)
				{
					return View(khachhang);
				}
			}
			return RedirectToAction("Login");
		}

		[HttpGet]
		[AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public IActionResult DangKyTaiKhoan()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("dang-ky.html", Name = "DangKy")]
		public async Task<IActionResult> DangKyTaiKhoan(RegisterVM taikhoan)
		{
			try
			{
				if(ModelState.IsValid)
				{
					string salt = Utilities.GetRandomKey();
					Customer khachhang = new Customer
					{
						FullName = taikhoan.FullName,
						Phone = taikhoan.Phone,
						Email = taikhoan.Email,
						AccPassword = (taikhoan.Password + salt.Trim()).ToMD5(),
						Active = true,
						Salt = salt,
						CreateDate = DateTime.Now,
					};
					try
					{
						_context.Add(khachhang);
						await _context.SaveChangesAsync();

						HttpContext.Session.SetString("CustomersId", khachhang.CustomersId.ToString());
						var taiKhoanID = HttpContext.Session.GetString("CustomersId");

						var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Name, khachhang.FullName),
							new Claim("CustomersId", khachhang.CustomersId.ToString())
						};
						ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
						ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
						await HttpContext.SignInAsync(claimsPrincipal);
                        _notyfService.Success("Đăng ký tài khoản thành công ");
                        return RedirectToAction("Dashboard", "Accounts_63135901");
					}catch (Exception ex)
					{
						return RedirectToAction("DangKyTaiKhoan", "Accounts_63135901");
					}
				}
				else
				{
					return View(taikhoan);
				}
			}
			catch
			{
				return View(taikhoan);
			}
		}


		[AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login(string returnUrl = null)
		{
			var taikhoanID = HttpContext.Session.GetString("CustomersId");
			if (taikhoanID != null)
			{
                return RedirectToAction("Dashboard", "Accounts_63135901");
			}
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

		[HttpPost]
		[AllowAnonymous]
		[Route("dang-nhap.html", Name = "DangNhap")]
		public async Task<IActionResult> Login(LoginViewModel customer, string returnUrl = null)
		{
            try
            {
                //if (ModelState.IsValid)
                //{
                    bool isEmail = Utilities.IsValidEmail(customer.UserName);
                    if (!isEmail)
                    {
                        return View(customer);
                    }

                    var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.UserName);

                    if (khachhang == null)
                    {
                        return RedirectToAction("DangKyTaiKhoan");
                    }
                    string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
                    if (khachhang.AccPassword != pass)
                    {
                        _notyfService.Warning("Thông tin đăng nhập không chính xác");
                        return View(customer);
                    }

                    //Kiểm tra tài khoản có bị disable không?
                    if (khachhang.Active == false)
                    {
                        return RedirectToAction("ThongBao", "Accounts_663135901");
                    }

                    //Lưu session vào MaKH
                    HttpContext.Session.SetString("CustomersId", khachhang.CustomersId.ToString());
                    var taikhoanID = HttpContext.Session.GetString("CustomersId");
                    //Identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachhang.FullName),
                        new Claim("CustomersId", khachhang.CustomersId.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đăng nhập thành công");
                    return RedirectToAction("Dashboard", "Accounts_63135901");
                //}
            }
            catch
            {
                return RedirectToAction("DangKyTaiKhoan", "Accounts_63135901");
            }
            _notyfService.Warning("Lỗi");
            return View(customer);
        }

		[HttpGet]
		[Route("dang-xuat.html", Name = "Logout")]
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync();
			HttpContext.Session.Remove("CustomersId");
			return RedirectToAction("Index", "Home_63135901");
		}
	}
}
