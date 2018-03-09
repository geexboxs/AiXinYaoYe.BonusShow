using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AiXinYaoYe.Database;
using AiXinYaoYe.Database.Entity;

namespace AiXinYaoYe.Areas.Admin.Controllers
{
    [Authorize]
    public class BonusProductController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Admin/BonusProduct
        public ActionResult Index()
        {
            return View(db.BonusProducts.ToList());
        }

        // GET: Admin/BonusProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BonusProduct bonusProduct = db.BonusProducts.Find(id);
            if (bonusProduct == null)
            {
                return HttpNotFound();
            }
            return View(bonusProduct);
        }

        // GET: Admin/BonusProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BonusProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Desc,Bonus,DetailPics,CoverImage")] BonusProduct bonusProduct)
        {
            if (ModelState.IsValid)
            {
                db.BonusProducts.Add(bonusProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bonusProduct);
        }

        // GET: Admin/BonusProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BonusProduct bonusProduct = db.BonusProducts.Find(id);
            if (bonusProduct == null)
            {
                return HttpNotFound();
            }
            return View(bonusProduct);
        }

        // POST: Admin/BonusProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Desc,Bonus,DetailPics,CoverImage")] BonusProduct bonusProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bonusProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bonusProduct);
        }

        // GET: Admin/BonusProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BonusProduct bonusProduct = db.BonusProducts.Find(id);
            if (bonusProduct == null)
            {
                return HttpNotFound();
            }
            return View(bonusProduct);
        }

        // POST: Admin/BonusProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BonusProduct bonusProduct = db.BonusProducts.Find(id);
            db.BonusProducts.Remove(bonusProduct);
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
