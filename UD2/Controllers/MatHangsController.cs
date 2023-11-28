using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UD2.Models;

namespace UD2.Controllers
{
    public class MatHangsController : Controller
    {
        private UD2Entities db = new UD2Entities();

        // GET: MatHangs
        public ActionResult Index()
        {
            var matHangs = db.MatHangs.Include(m => m.LoaiMatHang);
            return View(matHangs.ToList());
        }

        // GET: MatHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = db.MatHangs.Find(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            return View(matHang);
        }

        // GET: MatHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiMatHang = new SelectList(db.LoaiMatHangs, "MaLoaiMatHang", "TenLoaiMatHang");
            return View();
        }

        // POST: MatHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,TenHang,DonViTinh,DonGia,MaLoaiMatHang")] MatHang matHang)
        {
            if (ModelState.IsValid)
            {
                db.MatHangs.Add(matHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiMatHang = new SelectList(db.LoaiMatHangs, "MaLoaiMatHang", "TenLoaiMatHang", matHang.MaLoaiMatHang);
            return View(matHang);
        }

        // GET: MatHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = db.MatHangs.Find(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiMatHang = new SelectList(db.LoaiMatHangs, "MaLoaiMatHang", "TenLoaiMatHang", matHang.MaLoaiMatHang);
            return View(matHang);
        }

        // POST: MatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,TenHang,DonViTinh,DonGia,MaLoaiMatHang")] MatHang matHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiMatHang = new SelectList(db.LoaiMatHangs, "MaLoaiMatHang", "TenLoaiMatHang", matHang.MaLoaiMatHang);
            return View(matHang);
        }

        // GET: MatHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = db.MatHangs.Find(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            return View(matHang);
        }

        // POST: MatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MatHang matHang = db.MatHangs.Find(id);
            db.MatHangs.Remove(matHang);
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
