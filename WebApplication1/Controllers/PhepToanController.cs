using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class PhepToanController : Controller
    {
        // GET: PhepToan
        public ActionResult Index()
        {
            //ViewBag.AString = "Hello";
            //var message = new Models.Message();
            //message.Welcome = "Welcome to ASP.NET MVC";
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(double a, double b, string pt = "+")
        {
            switch (pt)
            {
                case "+": ViewBag.KQ = a + b; break;
                case "-": ViewBag.KQ = a - b; break;
                case "*": ViewBag.KQ = a * b; break;
                case "/":
                    if (b == 0) ViewBag.KQ = "Không chia được cho 0";
                    else ViewBag.KQ = a / b; break;
            }
            return View();
        }
    }
}