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
    public class RecommendProductController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Admin/RecommendProduct
        public ActionResult Index()
        {
            return View(db.RecommandProducts.ToList());
        }

        // GET: Admin/RecommendProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommandProduct recommandProduct = db.RecommandProducts.Find(id);
            if (recommandProduct == null)
            {
                return HttpNotFound();
            }
            return View(recommandProduct);
        }

        // GET: Admin/RecommendProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RecommendProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Desc,Price,DetailPics,CoverImage")] RecommandProduct recommandProduct)
        {
            if (ModelState.IsValid)
            {
                db.RecommandProducts.Add(recommandProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recommandProduct);
        }

        // GET: Admin/RecommendProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommandProduct recommandProduct = db.RecommandProducts.Find(id);
            if (recommandProduct == null)
            {
                return HttpNotFound();
            }
            return View(recommandProduct);
        }

        // POST: Admin/RecommendProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Desc,Price,DetailPics,CoverImage")] RecommandProduct recommandProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recommandProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recommandProduct);
        }

        // GET: Admin/RecommendProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommandProduct recommandProduct = db.RecommandProducts.Find(id);
            if (recommandProduct == null)
            {
                return HttpNotFound();
            }
            return View(recommandProduct);
        }

        // POST: Admin/RecommendProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecommandProduct recommandProduct = db.RecommandProducts.Find(id);
            db.RecommandProducts.Remove(recommandProduct);
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
