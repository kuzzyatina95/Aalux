using AaluxWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AaluxWeb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
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

        public ActionResult IndexCreateOrder()
        {
            NewOrderViewModel newOrder = new NewOrderViewModel();
            return Json(db.ClassCars.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult IndexCreateOrder(NewOrderViewModel orderNew)
        {

            
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    Direction = orderNew.Direction,
                    DatePost = DateTime.Now,
                    TimePost = DateTime.Now.TimeOfDay,
                    ClassCarId = orderNew.ClassCar.Id,
                    OrderStatusId = 1,
                    Client = orderNew.Client,
                    PaymentId = orderNew.PaymentId,
                    DateEnd = null,
                    TimeEnd = null
                };

                Client client = db.Clients.FirstOrDefault(c => c.Email == orderNew.Client.Email);
                if (client != null)
                {
                    order.Client = orderNew.Client;
                }
                Nullable<DateTime> date = null;


                order.TimeBegin = DateTime.Now.TimeOfDay;
                order.TimeBegin = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Seconds);
                string.Format("{0:hh\\:mm\\:ss}", order.TimeBegin);
                order.DateBegin = DateTime.Now;

                //db.Orders.Add(order);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(orderNew, JsonRequestBehavior.AllowGet);
        }
    }
}