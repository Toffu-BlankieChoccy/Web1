using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Controllers
{
    public class ViewBagSampleController : Controller
    {
        // GET: ViewBagSample
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        {
            ViewBag.Id = "SV001";
            ViewBag.Name = "Tran Hoang Trong";
            ViewData["Marks"] = 10;
            return View();
        }
    }
}