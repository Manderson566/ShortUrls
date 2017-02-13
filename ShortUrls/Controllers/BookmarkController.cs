using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShortUrls.Models;

namespace ShortUrls.Controllers
{
    public class BookmarkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookmark
        public ActionResult Index()
        {
            var bookmark = db.Bookmark.Include(b => b.Owner);
            return View(bookmark.ToList());
        }

        // GET: Bookmark/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmark.Find(id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            return View(bookmark);
        }

        // GET: Bookmark/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(db.Bookmark, "Id", "Email");
            return View();
        }

        // POST: Bookmark/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Created,Description,Title,Url,ShortUrl,OwnerId")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                db.Bookmark.Add(bookmark);
                bookmark.Created = DateTime.Now;
                bookmark.OwnerId = db.ApplicationUsers
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.Bookmark, "Id", "Email", bookmark.OwnerId);
            return View(bookmark);
        }

        // GET: Bookmark/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmark.Find(id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.ApplicationUsers, "Id", "Email", bookmark.OwnerId);
            return View(bookmark);
        }

        // POST: Bookmark/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Description,Title,Url,ShortUrl,OwnerId")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookmark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.ApplicationUsers, "Id", "Email", bookmark.OwnerId);
            return View(bookmark);
        }

        // GET: Bookmark/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmark.Find(id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            return View(bookmark);
        }

        // POST: Bookmark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = db.Bookmark.Find(id);
            db.Bookmark.Remove(bookmark);
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
