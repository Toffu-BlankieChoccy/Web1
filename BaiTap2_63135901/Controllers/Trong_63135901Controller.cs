using BaiTap2_63135901.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap2_63135901.Controllers
{
    public class Trong_63135901Controller : Controller
    {
        // GET: Trong_63135901
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register2(FormCollection field)
        {
            ViewBag.Id = field["Id"];
            ViewBag.Name = field["Name"];
            ViewBag.Marks = field["Marks"];
            return View(ViewBag);
        }

        ///Sử dụng đối số Action
        public ActionResult Index3()
        {
            return View();
        }
        public ActionResult Register3(string Id, string Name, double Marks)
        {
            ViewBag.Id = Id;
            ViewBag.Name = Name;
            ViewBag.Marks = Marks;
            return View(ViewBag);
        }


        ///Sử dụng model
        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult Register4(StudentModels s)
        {
            ViewBag.Id = s.Id;
            ViewBag.Name = s.Name;
            ViewBag.Marks = s.Marks;
            return View(ViewBag);
        }

        ///Sử dụng đối số Request
        public ActionResult Index5()
        {
            return View();
        }
        public ActionResult Register5(string Id, string Name, double Marks)
        {
            ViewBag.Id = Request.Form["Id"];
            ViewBag.Name = Request.Form["Name"];
            ViewBag.Marks = Request.Form["Marks"];
            return View(ViewBag);
        }
    }
}