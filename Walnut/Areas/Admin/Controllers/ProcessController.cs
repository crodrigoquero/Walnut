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
    public class ProcessController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Process
        public async Task<ActionResult> Index()
        {
            return View(await db.Processes.ToListAsync());
        }

        // GET: Admin/Process/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await db.Processes.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        // GET: Admin/Process/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Process/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,ProcessTemplateId,StartDate,ActualStartDate,Deadline,ActualEndDate")] Process process)
        {
            if (ModelState.IsValid)
            {
                db.Processes.Add(process);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(process);
        }

        // GET: Admin/Process/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await db.Processes.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        // POST: Admin/Process/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,ProcessTemplateId,StartDate,ActualStartDate,Deadline,ActualEndDate")] Process process)
        {
            if (ModelState.IsValid)
            {
                db.Entry(process).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(process);
        }

        // GET: Admin/Process/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await db.Processes.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        // POST: Admin/Process/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Process process = await db.Processes.FindAsync(id);
            db.Processes.Remove(process);
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
