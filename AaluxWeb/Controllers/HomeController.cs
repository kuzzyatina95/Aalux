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

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(NewOrderViewModel orderNew)
        {
            if (orderNew == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    ClassCarId = orderNew.ClassCarId,
                    Client = orderNew.Client,
                    DateBegin = orderNew.DateBegin,
                    TimeBegin = orderNew.TimeBegin,
                    OrderStatusId = 1,
                    PaymentId = orderNew.PaymentId,
                    DatePost = DateTime.Now,
                    TimePost = Convert.ToDateTime(DateTime.Now.ToString("HH:mm")),
                    Price = Double.Parse(orderNew.Price, System.Globalization.CultureInfo.InvariantCulture),
                    Direction = new Direction()
                    {
                        AddressDestination = orderNew.Direction.AddressDestination,
                        AddressOrigin = orderNew.Direction.AddressOrigin,
                        LatOrigin = orderNew.Direction.LatOrigin,
                        LngOrigin = orderNew.Direction.LngOrigin,
                        LatDestination = orderNew.Direction.LatDestination,
                        LngDestination = orderNew.Direction.LngDestination
                    }
                };
                Client client = db.Clients.FirstOrDefault(c => c.Email == orderNew.Client.Email);
                if (client != null)
                {
                    order.Client = client;
                }
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderNew);
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
    }
}