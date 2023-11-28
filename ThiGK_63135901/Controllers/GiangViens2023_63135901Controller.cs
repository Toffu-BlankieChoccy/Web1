using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThiGK_63135901.Models;

namespace ThiGK_63135901.Controllers
{
    public class GiangViens2023_63135901Controller : Controller
    {
        private ThiGK_63135901Entities db = new ThiGK_63135901Entities();
        string LayMaGV()
        {
            var maMax = db.GiangViens.ToList().Select(n => n.MaGV).Max();
            int maGV = int.Parse(maMax.Substring(2)) + 1;
            string GV = String.Concat("00", maGV.ToString());
            return "GV" + GV.Substring(maGV.ToString().Length - 1);
        }

        [HttpGet]
        public ActionResult TimKiem_63135901(string maGV = "", string maBM = "")
        {
            ViewBag.maGV = maGV;
            ViewBag.maBM = new SelectList(db.BoMons, "MaBM", "TenBM");
            var giangViens = db.GiangViens.SqlQuery("GiangVien_TimKiem'" + maGV + "','" + maBM + "'");
            if (giangViens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(giangViens.ToList());
        }

        public ActionResult GioiThieu_63135901()
        {
            return View();
        }


        // GET: GiangViens2023_63135901
        public ActionResult Index()
        {
            var giangViens = db.GiangViens.Include(g => g.BoMon);
            return View(giangViens.ToList());
        }

        // GET: GiangViens2023_63135901/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = db.GiangViens.Find(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            return View(giangVien);
        }

        // GET: GiangViens2023_63135901/Create
        public ActionResult Create()
        {
            ViewBag.MaGV = LayMaGV();
            ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM");
            return View();
        }

        // POST: GiangViens2023_63135901/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGV,HoGV,TenGV,GioiTinh,NgaySinh,Email,AnhGV,MaBM")] GiangVien giangVien)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgGV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgGV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgGV.SaveAs(path);

            if (ModelState.IsValid)
            {
                giangVien.MaGV = LayMaGV();
                giangVien.AnhGV = postedFileName;
                db.GiangViens.Add(giangVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM", giangVien.MaBM);
            return View(giangVien);
        }

        // GET: GiangViens2023_63135901/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = db.GiangViens.Find(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM", giangVien.MaBM);
            return View(giangVien);
        }

        // POST: GiangViens2023_63135901/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGV,HoGV,TenGV,GioiTinh,NgaySinh,Email,AnhGV,MaBM")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giangVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM", giangVien.MaBM);
            return View(giangVien);
        }

        // GET: GiangViens2023_63135901/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = db.GiangViens.Find(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            return View(giangVien);
        }

        // POST: GiangViens2023_63135901/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GiangVien giangVien = db.GiangViens.Find(id);
            db.GiangViens.Remove(giangVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
