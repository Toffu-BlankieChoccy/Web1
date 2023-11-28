using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap5_63135901.Controllers
{
    public class ChangeBanner_63135901Controller : Controller
    {
        // GET: ChangeBanner_63135901
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangeBanner()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangeBanner(HttpPostedFileBase banner)
        {
            string postedFileName =
           System.IO.Path.GetFileName(banner.FileName);
            var path = Server.MapPath("/Images/" + postedFileName);
            banner.SaveAs(path);
            string fSave = Server.MapPath("/banner.txt");
            System.IO.File.WriteAllText(fSave, postedFileName);
            return View();
        }

    }
}