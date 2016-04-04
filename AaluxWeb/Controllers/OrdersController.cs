using AaluxWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace AaluxWeb.Controllers
{
    [Authorize(Roles = "TechAdmin, Admin")]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Orders
        public ActionResult Index()
        {
            TabOrdersViewModel tabOrders = new TabOrdersViewModel()
            {
                OrdersNew = db.Orders.Where(o => o.OrderStatusId == 1)
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client),

                OrdersInProgress = db.Orders.Where(o => o.OrderStatusId == 2)
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client),

                OrdersFinished = db.Orders.Where(o => o.OrderStatusId == 3)
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client),

                OrdersCanceled = db.Orders.Where(o => o.OrderStatusId == 4)
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client)
            };
            return View(tabOrders);
        }
    }
}