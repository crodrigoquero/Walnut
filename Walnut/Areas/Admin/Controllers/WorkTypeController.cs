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
    public class WorkTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/WorkType
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkTypes.ToListAsync());
        }

        // GET: Admin/WorkType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = await db.WorkTypes.FindAsync(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // GET: Admin/WorkType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/WorkType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] WorkType workType)
        {
            if (ModelState.IsValid)
            {
                db.WorkTypes.Add(workType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(workType);
        }

        // GET: Admin/WorkType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = await db.WorkTypes.FindAsync(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // POST: Admin/WorkType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] WorkType workType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workType);
        }

        // GET: Admin/WorkType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = await db.WorkTypes.FindAsync(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // POST: Admin/WorkType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkType workType = await db.WorkTypes.FindAsync(id);
            db.WorkTypes.Remove(workType);
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
