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

        [Route("U/{userName}")]
        public ActionResult Details(string UserName)
        {
            ApplicationUser userInstance = db.Users.Where(u => u.UserName == UserName).FirstOrDefault();
            return View(userInstance);
        }
    }
}