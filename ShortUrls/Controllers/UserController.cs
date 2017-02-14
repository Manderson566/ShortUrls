using Microsoft.AspNet.Identity;
using ShortUrls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortUrls.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User


        public ActionResult Index()
        {
            return View();

        }
        [Route("U/{userName}")]
        public ActionResult Details(string UserName)
        {
            ApplicationUser userInstance = db.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            ViewBag.PublicBookmarks = db.Bookmark.Where(b => b.Public == true).Where(u => u.Owner.UserName == UserName).ToList().OrderByDescending(o => o.Created);
            return View(userInstance);
        }

        [HttpPost]
        [Route("u/{userName}")]
        public ActionResult AddFriend(string userName)
        {

            string me = User.Identity.GetUserId();
            string target = db.Users.Where(u => u.UserName == userName).FirstOrDefault().Id;
            Friend relationship = new Friend
            {
                RequestorId = me,
                TargetId = target
            };
            db.Friend.Add(relationship);
            db.SaveChanges();
            return RedirectToAction("Details");
        }
    }
}