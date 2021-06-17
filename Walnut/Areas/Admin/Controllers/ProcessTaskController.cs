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
    public class ProcessTaskController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProcessTask
        public async Task<ActionResult> Index()
        {
            return View(await db.ProcessTasks.ToListAsync());
        }

        // GET: Admin/ProcessTask/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTask processTask = await db.ProcessTasks.FindAsync(id);
            if (processTask == null)
            {
                return HttpNotFound();
            }
            return View(processTask);
        }

        // GET: Admin/ProcessTask/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProcessTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,ProcessId,ProcessStageId,ProcessTaskTemplateId,TaskTypeId,StartDate,ActualStartDate,Deadline,ActualEndDate")] ProcessTask processTask)
        {
            if (ModelState.IsValid)
            {
                db.ProcessTasks.Add(processTask);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(processTask);
        }

        // GET: Admin/ProcessTask/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTask processTask = await db.ProcessTasks.FindAsync(id);
            if (processTask == null)
            {
                return HttpNotFound();
            }
            return View(processTask);
        }

        // POST: Admin/ProcessTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,ProcessId,ProcessStageId,ProcessTaskTemplateId,TaskTypeId,StartDate,ActualStartDate,Deadline,ActualEndDate")] ProcessTask processTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processTask).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(processTask);
        }

        // GET: Admin/ProcessTask/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTask processTask = await db.ProcessTasks.FindAsync(id);
            if (processTask == null)
            {
                return HttpNotFound();
            }
            return View(processTask);
        }

        // POST: Admin/ProcessTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProcessTask processTask = await db.ProcessTasks.FindAsync(id);
            db.ProcessTasks.Remove(processTask);
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
