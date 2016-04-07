using AaluxWeb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;

namespace AaluxWeb.Controllers
{
    public class DriverProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DriverProfile
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Car(int? Id)
        {
            Car car = await db.Cars.FirstOrDefaultAsync(u => u.Id == Id);

            
            if (car == null)
            {
                ViewBag.ClassCarId = new SelectList(db.ClassCars, "Id", "Name");
                return View();
            }
            CarViewModel carVM = new CarViewModel()
            {
                BodyType = car.BodyType,
                Id = car.Id,
                Capacity = car.Capacity,
                ClassCar = car.ClassCar,
                ClassCarId = car.ClassCarId,
                Color = car.Color,
                ImageLink = car.ImageLink,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                ShortCharacter = car.ShortCharacter,
                YearOfRelease = car.YearOfRelease
            };
            ViewBag.ClassCarId = new SelectList(db.ClassCars, "Id", "Name", car.ClassCarId);
            return View(carVM);
        }

        [HttpPost]
        public async Task<ActionResult> Car(CarViewModel carVM)
        {
            Car car1 = await db.Cars.FirstOrDefaultAsync(u => u.Id == carVM.Id);

            string UserId = User.Identity.GetUserId();
            Driver Driver = await db.Drivers.FirstOrDefaultAsync(u => u.Id == UserId);
            if (car1 == null)
            {
                if (ModelState.IsValid)
                {
                    Car car = new Car()
                    {
                        ClassCarId = carVM.ClassCarId,
                        BodyType = carVM.BodyType,
                        Capacity = carVM.Capacity,
                        Color = carVM.Color,
                        ImageLink = carVM.ImageLink,
                        Manufacturer = carVM.Manufacturer,
                        Model = carVM.Model,
                        ShortCharacter = carVM.ShortCharacter,
                        YearOfRelease = carVM.YearOfRelease
                    };
                    Driver.Car = car;
                    db.Entry(Driver).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.ClassCarId = new SelectList(db.ClassCars, "Id", "Name", carVM.ClassCarId);
                return View(carVM);
            }
            if (ModelState.IsValid)
            {
                car1.ClassCarId = carVM.ClassCarId;
                car1.BodyType = carVM.BodyType;
                car1.Capacity = carVM.Capacity;
                car1.Color = carVM.Color;
                car1.ImageLink = carVM.ImageLink;
                car1.Manufacturer = carVM.Manufacturer;
                car1.Model = carVM.Model;
                car1.ShortCharacter = carVM.ShortCharacter;
                car1.YearOfRelease = carVM.YearOfRelease;

                db.Entry(car1).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClassCarId = new SelectList(db.ClassCars, "Id", "Name", car1.ClassCarId);
            return View(carVM);
        }


        [Authorize(Roles = "TechAdmin, Admin, Driver")]
        public ActionResult DriverNavInfo()
        {
                 
            string UserId = User.Identity.GetUserId();
            NavDriverViewModel navDriver = new NavDriverViewModel();

            navDriver.Driver = db.Drivers.Include(d => d.User).Include(c=>c.Car).FirstOrDefault(d => d.Id == UserId);
            navDriver.Orders = db.Orders.Where(o => o.DriverId == UserId);


            return PartialView("_DriverNavPartial", navDriver);
        }

        // GET: OrderStatus/Edit/5

        [Authorize(Roles = "TechAdmin, Admin, Driver")]
        public async Task<ActionResult> EditDriver(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = await db.Drivers.FindAsync(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            EditDriverViewModel editDriver = new EditDriverViewModel()
            {
                Id = driver.Id,
                Name = driver.Name,
                Surname = driver.Surname,
                Email = driver.Email,
                Birthday = driver.Birthday,
                IsAvailable = driver.IsAvailable,
                Phone = driver.Phone
            };
            return View(editDriver);
        }

        // POST: OrderStatus/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "TechAdmin, Admin, Driver")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDriver([Bind(Include = "Id,Name,Surname,Email,Birthday,IsAvailable,Phone")] EditDriverViewModel editDriver)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (editDriver.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = await db.Drivers.FindAsync(editDriver.Id);
            if (ModelState.IsValid)
            {
                driver.Name = editDriver.Name;
                driver.Surname = editDriver.Surname;
                driver.Email = editDriver.Email;
                driver.Birthday = editDriver.Birthday;
                driver.IsAvailable = editDriver.IsAvailable;
                driver.Phone = editDriver.Phone;
                db.Entry(driver).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(editDriver);
        }

        public ActionResult Orders()
        {
            string UserId = User.Identity.GetUserId();

            DriverOrdersViewModel driverOrders = new DriverOrdersViewModel()
            {
                AllOrders = db.Orders.Where(o => o.DriverId == UserId)
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client),

                CurrentOrders = db.Orders.Where(o => o.DriverId == UserId).Where(o=>o.OrderStatusId==2)
                .Include(d => d.Driver)
                .Include(c => c.ClassCar)
                .Include(d => d.Direction)
                .Include(o => o.OrderStatus)
                .Include(p => p.Payment)
                .Include(c => c.Client)
            };

            return View(driverOrders);
        }
    }
}