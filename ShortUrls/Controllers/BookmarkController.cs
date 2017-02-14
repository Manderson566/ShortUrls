using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShortUrls.Models;
using Microsoft.AspNet.Identity;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ShortUrls.Controllers
{
   [Authorize]
    public class BookmarkController : Controller
    {
        private static string ShortUrl(string Url)
        {

            byte[] byteData = Encoding.UTF8.GetBytes(Url);
            Stream inputStream = new MemoryStream(byteData);
            using (SHA256 shall = new SHA256Managed())
            {
                var result = shall.ComputeHash(inputStream);
                string output = BitConverter.ToString(result);
                string Hashed = (output.Replace("-", "").Substring(0, 5));
                return Hashed;
            }
            
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookmark
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var bookmark = db.Bookmark.Include(b => b.Owner).Where(b => b.OwnerId == userId).OrderByDescending(o => o.Created); ;
            return View(bookmark.ToList());
        }

        // GET: Bookmark/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Bookmark bookmark = db.Bookmark.Where(b => b.OwnerId == userId).Where(b => b.Id == id).FirstOrDefault();
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            return View(bookmark);
        }

        // GET: Bookmark/Create
        [Authorize]
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
        public ActionResult Create([Bind(Include = "Id,Created,Description,Title,Url,ShortUrl,OwnerId,Public")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                db.Bookmark.Add(bookmark);
                bookmark.Created = DateTime.Now;
                bookmark.OwnerId = User.Identity.GetUserId();
                bookmark.ShortUrl = ShortUrl(bookmark.Url);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.Bookmark, "Id", "Email", bookmark.OwnerId);
            return View(bookmark);
        }

        // GET: Bookmark/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Bookmark bookmark = db.Bookmark.Where(b => b.OwnerId == userId).Where(b => b.Id == id).FirstOrDefault();
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            return View(bookmark);
        }

        // POST: Bookmark/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Description,Title,Url,ShortUrl,OwnerId,Public")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                db.Bookmark.Add(bookmark);
                bookmark.Created = DateTime.Now;
                bookmark.OwnerId = User.Identity.GetUserId();
                bookmark.ShortUrl = ShortUrl(bookmark.Url);
                db.Entry(bookmark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.ApplicationUsers, "Id", "Email", bookmark.OwnerId);
            return View(bookmark);
        }

        // GET: Bookmark/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Bookmark bookmark = db.Bookmark.Where(b => b.OwnerId == userId).Where(b => b.Id == id).FirstOrDefault();
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            return View(bookmark);
        }

        // POST: Bookmark/Delete/5
        [Authorize]
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
