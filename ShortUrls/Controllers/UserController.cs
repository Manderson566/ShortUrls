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
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User

        [Route("U/{userName}")]
        public ActionResult Details(string UserName)
        {
            ApplicationUser userInstance = db.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            ViewBag.PublicBookmarks = db.Bookmark.Where(b => b.Public == true).Where(u => u.Owner.UserName == UserName).ToList().OrderByDescending(o => o.Created);
            if (userInstance == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string me = User.Identity.GetUserId();        
            string target = db.Users.Where(u => u.UserName == UserName).FirstOrDefault().Id;

            bool friend = db.Friend.Where( f => (f.RequestorId == me && f.TargetId == target) && (f.TargetId == me && f.RequestorId == target)).Any();
            bool requestSent = db.Friend.Where(f => f.RequestorId == me && f.TargetId == target).Any();
            bool like = db.Like.Where(f=> f.RequestorId == me && f.TargetId == target).Any();

            ViewBag.LikeBookMark = db.Like.Where(l => l.TargetId == UserName).FirstOrDefault();


            ViewBag.Friends = friend;
            ViewBag.RequestSent = requestSent;
            ViewBag.Likes = like;

            
            return View(userInstance);
        }

        [HttpPost]
        [Route("u/{userName}")]
        public ActionResult AddFriend(string userName, int? id)
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


            Like Liked = new Like
            {
                RequestorId = me,
                TargetId = target
            };
            db.Like.Add(Liked);
            db.SaveChanges();
            return RedirectToAction("Details");
           
        }
    }
}