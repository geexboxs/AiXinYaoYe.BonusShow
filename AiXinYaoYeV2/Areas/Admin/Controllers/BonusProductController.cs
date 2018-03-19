﻿using System.Linq;
using System.Net;
using AiXinYaoYeV2.Database;
using AiXinYaoYeV2.Database.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiXinYaoYeV2.Areas.Admin.Controllers
{
    [Authorize]
    public class BonusProductController : Controller
    {
        private MyDbContext db;

        public BonusProductController(MyDbContext db)
        {
            this.db = db;
        }

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
                return BadRequest();
            }
            BonusProduct bonusProduct = db.BonusProducts.Find(id);
            if (bonusProduct == null)
            {
                return NotFound();
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
        public ActionResult Create([Bind("Id,Name,Desc,Bonus,DetailPics,CoverImage")] BonusProduct bonusProduct)
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
                return BadRequest();
            }
            BonusProduct bonusProduct = db.BonusProducts.Find(id);
            if (bonusProduct == null)
            {
                return NotFound();
            }
            return View(bonusProduct);
        }

        // POST: Admin/BonusProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name,Desc,Bonus,DetailPics,CoverImage")] BonusProduct bonusProduct)
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
                return BadRequest();
            }
            BonusProduct bonusProduct = db.BonusProducts.Find(id);
            if (bonusProduct == null)
            {
                return NotFound();
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