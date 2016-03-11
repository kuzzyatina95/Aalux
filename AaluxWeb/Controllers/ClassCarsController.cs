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
    public class ClassCarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClassCars
        public async Task<ActionResult> Index()
        {
            return View(await db.ClassCars.ToListAsync());
        }

        // GET: ClassCars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassCar classCar = await db.ClassCars.FindAsync(id);
            if (classCar == null)
            {
                return HttpNotFound();
            }
            return View(classCar);
        }

        // GET: ClassCars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassCars/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price")] ClassCar classCar)
        {
            if (ModelState.IsValid)
            {
                db.ClassCars.Add(classCar);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(classCar);
        }

        // GET: ClassCars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassCar classCar = await db.ClassCars.FindAsync(id);
            if (classCar == null)
            {
                return HttpNotFound();
            }
            return View(classCar);
        }

        // POST: ClassCars/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price")] ClassCar classCar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classCar).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(classCar);
        }

        // GET: ClassCars/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassCar classCar = await db.ClassCars.FindAsync(id);
            if (classCar == null)
            {
                return HttpNotFound();
            }
            return View(classCar);
        }

        // POST: ClassCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClassCar classCar = await db.ClassCars.FindAsync(id);
            db.ClassCars.Remove(classCar);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
