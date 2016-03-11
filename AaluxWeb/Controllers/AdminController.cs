using AaluxWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AaluxWeb.Controllers
{
    [Authorize(Roles = "TechAdmin, Admin")]
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
    }
}