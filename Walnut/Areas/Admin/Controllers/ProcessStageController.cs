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
    public class ProcessStageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProcessStage
        public async Task<ActionResult> Index()
        {
            return View(await db.ProcessStages.ToListAsync());
        }

        // GET: Admin/ProcessStage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStage processStage = await db.ProcessStages.FindAsync(id);
            if (processStage == null)
            {
                return HttpNotFound();
            }
            return View(processStage);
        }

        // GET: Admin/ProcessStage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProcessStage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,ProcessId,StartDate,ActualStartDate,Deadline,ActualEndDate")] ProcessStage processStage)
        {
            if (ModelState.IsValid)
            {
                db.ProcessStages.Add(processStage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(processStage);
        }

        // GET: Admin/ProcessStage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStage processStage = await db.ProcessStages.FindAsync(id);
            if (processStage == null)
            {
                return HttpNotFound();
            }
            return View(processStage);
        }

        // POST: Admin/ProcessStage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,ProcessId,StartDate,ActualStartDate,Deadline,ActualEndDate")] ProcessStage processStage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processStage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(processStage);
        }

        // GET: Admin/ProcessStage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStage processStage = await db.ProcessStages.FindAsync(id);
            if (processStage == null)
            {
                return HttpNotFound();
            }
            return View(processStage);
        }

        // POST: Admin/ProcessStage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProcessStage processStage = await db.ProcessStages.FindAsync(id);
            db.ProcessStages.Remove(processStage);
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
