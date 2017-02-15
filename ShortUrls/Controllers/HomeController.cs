using Microsoft.AspNet.Identity;
using ShortUrls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShortUrls.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.PublicBookmarks = db.Bookmark.Where(b => b.Public == true).ToList().OrderByDescending(o => o.Created); 
            return View();

        }

        public ActionResult Details(int? id)

        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmark.Where(b => b.Public == true).Where(b => b.Id == id).FirstOrDefault();
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            Click click = new Click();
            click.BookmarkId = bookmark.Id;
            db.Click.Add(click);
            click.Created = DateTime.Now;
            bookmark.Clicks++;
            db.SaveChanges();
            return View(bookmark);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("R/{ShortUrl}")]
        public ActionResult Details(string ShortUrl)
        {
            Bookmark UrlInstance = db.Bookmark.Where(u => u.ShortUrl == ShortUrl).FirstOrDefault();
            Click click = new Click();
            Bookmark bookmark = db.Bookmark.Where(b => b.Public == true).Where(b => b.ShortUrl == ShortUrl).FirstOrDefault();
            click.BookmarkId = bookmark.Id;
            db.Click.Add(click);
            click.Created = DateTime.Now;
            bookmark.Clicks++;
            db.SaveChanges();
            return View(UrlInstance);
        }
    }
}