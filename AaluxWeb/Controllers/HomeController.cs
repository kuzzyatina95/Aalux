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

        [HttpPost]
        public ActionResult IndexCreateOrder(NewOrderViewModel orderNew)
        {

            
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    //Direction = orderNew.Direction,
                    DatePost = DateTime.Now,
                    //TimePost = DateTime.Now.TimeOfDay,
                    //ClassCarId = orderNew.ClassCar.Id,
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

                //order.TimeBegin = DateTime.Now.TimeOfDay;
                //order.TimeBegin = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Seconds);
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