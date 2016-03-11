using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AaluxWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AaluxWeb.Controllers
{
    public class PersonRoleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();   
        // GET: PersonRole
        public ActionResult Index()
        {
            var rolelist = db.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
             new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;

            var userlist = db.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
            new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userlist;
            var user = db.Users;
            //var testroles = db.Roles.Where(x => x.Users.Select(y => y.UserId).Contains(userId)).ToList();
            ViewBag.Message = "";

            return View();
            
        }
    }
}