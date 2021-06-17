using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Walnut.Entities;
using Walnut.Models;

namespace Walnut.Areas.Admin.Controllers
{
    public class PeriodicityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Periodicity
        public async Task<ActionResult> Index()
        {
            return View(await db.Periodicities.ToListAsync());
        }

        // GET: Admin/Periodicity/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodicity periodicity = await db.Periodicities.FindAsync(id);
            if (periodicity == null)
            {
                return HttpNotFound();
            }
            return View(periodicity);
        }

        // GET: Admin/Periodicity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Periodicity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] Periodicity periodicity)
        {
            if (ModelState.IsValid)
            {
                db.Periodicities.Add(periodicity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(periodicity);
        }

        // GET: Admin/Periodicity/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodicity periodicity = await db.Periodicities.FindAsync(id);
            if (periodicity == null)
            {
                return HttpNotFound();
            }
            return View(periodicity);
        }

        // POST: Admin/Periodicity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] Periodicity periodicity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periodicity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(periodicity);
        }

        // GET: Admin/Periodicity/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodicity periodicity = await db.Periodicities.FindAsync(id);
            if (periodicity == null)
            {
                return HttpNotFound();
            }
            return View(periodicity);
        }

        // POST: Admin/Periodicity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Periodicity periodicity = await db.Periodicities.FindAsync(id);
            db.Periodicities.Remove(periodicity);
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
