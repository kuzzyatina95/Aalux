using AaluxWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;

namespace AaluxWeb.Controllers
{
    [Authorize(Roles = "TechAdmin, Admin, Driver")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Admin
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client);
            return View(await orders.ToListAsync());
        }

        [Authorize(Roles = "TechAdmin, Admin")]
        public ActionResult AdminNavInfo()
        {
            TabsViewModel all = new TabsViewModel()
            {
                Drivers = db.Drivers.Include(u => u.User).Where(d=>d.IsAvailable==true),
                Orders = db.Orders.Where(o=>o.OrderStatusId==1),
                Licenses = db.Licenses,
                Users = db.Users.Where(u=>u.IsEnabled==false)
            };
            return PartialView("_AdminNavPartial", all);
        }

       
    }
}