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
    public class LicensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Licenses
        public async Task<ActionResult> Index()
        {
            var licenses = db.Licenses.Include(l => l.Driver);
            return View(await licenses.ToListAsync());
        }

        // GET: Licenses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = await db.Licenses.FindAsync(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            return View(license);
        }

        // GET: Licenses/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Drivers.Include(u=>u.User), "Id", "FullName");
            return View();
        }

        // POST: Licenses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,UserID,DateBegin,DateEnd")] License license)
        {
            if (ModelState.IsValid)
            {
                db.Licenses.Add(license);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Drivers.Include(u => u.User), "Id", "FullName", license.UserID);
            return View(license);
        }

        // GET: Licenses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = await db.Licenses.FindAsync(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Drivers.Include(u => u.User), "Id", "FullName", license.UserID);
            return View(license);
        }

        // POST: Licenses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,UserID,DateBegin,DateEnd")] License license)
        {
            if (ModelState.IsValid)
            {
                db.Entry(license).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Drivers.Include(u => u.User), "Id", "FullName", license.UserID);
            return View(license);
        }

        // GET: Licenses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = await db.Licenses.FindAsync(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            return View(license);
        }

        // POST: Licenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            License license = await db.Licenses.FindAsync(id);
            db.Licenses.Remove(license);
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
