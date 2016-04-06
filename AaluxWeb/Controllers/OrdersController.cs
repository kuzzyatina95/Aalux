using AaluxWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;

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

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            EditOrderViewModel editOrder = new EditOrderViewModel()
            {
                Id = order.Id,
                ClassCar = order.ClassCar,
                ClassCarId = order.ClassCarId,
                Client = order.Client,
                ClientId = order.ClientId,
                DateBegin = order.DateBegin,
                DateEnd = order.DateEnd,
                DatePost = order.DatePost,
                Direction = order.Direction,
                DirectionId = order.DirectionId,
                DriverId = order.DriverId,
                OrderStatusId = order.OrderStatusId,
                Payment = order.Payment,
                PaymentId = order.PaymentId,
                Price = order.Price,
                TimeBegin = order.TimeBegin,
                TimeEnd = order.TimeEnd,
                TimePost = order.TimePost,
                Driver = order.Driver
            };
            ViewBag.DriverId = new SelectList(
                db.Cars
                .Include(u => u.Driver)
                .Where(d => d.Driver.IsAvailable == true)
                .Where(d => d.Driver.IsBusy == false)
                .Where(c=>c.ClassCarId==order.ClassCarId), "UserId", "FullName", order.DriverId);
            //ViewBag.DriverId = new SelectList(db.Drivers.Include(u => u.User).Where(d=>d.IsAvailable==true).Where(d=>d.IsBusy==false), "Id", "FullName", order.DriverId);
            ViewBag.OrderStatusId = new SelectList(db.OrderStatuss, "Id", "Name", order.OrderStatusId);
            return View(editOrder);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DriverId,OrderStatusId,TimeEnd,DateEnd")] EditOrderViewModel editOrder)
        {
            if (editOrder == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client).FirstOrDefaultAsync(o => o.Id == editOrder.Id);
            
            if (order == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                if(editOrder.DriverId == null)
                {
                    Driver driver = await db.Drivers.FirstOrDefaultAsync(o => o.Id == order.DriverId);
                    if (driver != null)
                    {
                        driver.IsBusy = false;
                        db.Entry(driver).State = EntityState.Modified;
                    }
                    if(editOrder.OrderStatusId == 2 || editOrder.OrderStatusId == 3)
                    {
                        editOrder.OrderStatusId = 1;
                    }
                }
                if (editOrder.DriverId != null)
                {
                    switch (editOrder.OrderStatusId)
                    {
                        case 1:
                            {
                                editOrder.DriverId = null;
                                Driver driver = await db.Drivers.FirstOrDefaultAsync(o => o.Id == order.DriverId);
                                if (driver != null)
                                {
                                    driver.IsBusy = false;
                                    db.Entry(driver).State = EntityState.Modified;
                                }
                                break;
                            }
                        case 2:
                            {
                                Driver driver = await db.Drivers.FirstOrDefaultAsync(o => o.Id == editOrder.DriverId);
                                driver.IsBusy = true;
                                db.Entry(driver).State = EntityState.Modified;
                                break;
                            }
                        case 3:
                            {
                                Driver driver = await db.Drivers.FirstOrDefaultAsync(o => o.Id == editOrder.DriverId);
                                driver.IsBusy = false;
                                db.Entry(driver).State = EntityState.Modified;
                                break;
                            }
                        case 4:
                            {
                                Driver driver = await db.Drivers.FirstOrDefaultAsync(o => o.Id == editOrder.DriverId);
                                driver.IsBusy = false;
                                db.Entry(driver).State = EntityState.Modified;
                                break;
                            }
                    }
                }


                order.DriverId = editOrder.DriverId;
                order.OrderStatusId = editOrder.OrderStatusId;
                order.DateEnd = editOrder.DateEnd;
                order.TimeEnd = editOrder.TimeEnd;
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(editOrder);
        }
    }
}