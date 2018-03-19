using System.Linq;
using System.Net;
using AiXinYaoYeV2.Database;
using AiXinYaoYeV2.Database.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiXinYaoYeV2.Areas.Admin.Controllers
{
    [Authorize]
    public class RecommendProductController : Controller
    {
        private MyDbContext db;

        public RecommendProductController(MyDbContext db)
        {
            this.db = db;
        }

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
                return BadRequest();
            }
            RecommandProduct recommandProduct = db.RecommandProducts.Find(id);
            if (recommandProduct == null)
            {
                return NotFound();
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
        public ActionResult Create([Bind("Id,Name,Desc,Price,DetailPics,CoverImage")] RecommandProduct recommandProduct)
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
                return BadRequest();
            }
            RecommandProduct recommandProduct = db.RecommandProducts.Find(id);
            if (recommandProduct == null)
            {
                return NotFound();
            }
            return View(recommandProduct);
        }

        // POST: Admin/RecommendProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name,Desc,Price,DetailPics,CoverImage")] RecommandProduct recommandProduct)
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
                return BadRequest();
            }
            RecommandProduct recommandProduct = db.RecommandProducts.Find(id);
            if (recommandProduct == null)
            {
                return NotFound();
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
