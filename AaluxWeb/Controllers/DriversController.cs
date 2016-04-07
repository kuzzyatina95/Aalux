using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AaluxWeb.Models;

namespace AaluxWeb.Controllers
{
    [Authorize(Roles = "TechAdmin, Admin")]
    public class DriversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Drivers
        public async Task<ActionResult> Index()
        {
            var drivers = db.Drivers.Include(d => d.User);
            return View(await drivers.ToListAsync());
        }

        // GET: Drivers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Driver driver = await db.Drivers.FindAsync(id);
            Driver driver = await db.Drivers.Include(u => u.User).Include(c=>c.Car).Include(c=>c.Car.ClassCar).FirstOrDefaultAsync(d => d.Id == id);

    
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        //// GET: Drivers/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "Email");
        //    return View();
        //}

        //// POST: Drivers/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,Surname,Email,Phone,IsAvailable,IsBusy,IsFired,Birthday")] Driver driver)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Drivers.Add(driver);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "Email", driver.Id);
        //    return View(driver);
        //}

        // GET: Drivers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Driver driver = await db.Drivers.FindAsync(id);
            Driver driver = await db.Drivers.Include(u=>u.User).FirstOrDefaultAsync(d=>d.Id==id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", driver.Id);
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Surname,Email,Phone,IsAvailable,IsBusy,IsFired,Birthday")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", driver.Id);
            return View(driver);
        }

        //// GET: Drivers/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Driver driver = await db.Drivers.FindAsync(id);
        //    if (driver == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(driver);
        //}

        //// POST: Drivers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    Driver driver = await db.Drivers.FindAsync(id);
        //    db.Drivers.Remove(driver);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
