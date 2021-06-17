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
    public class TaskTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/TaskType
        public async Task<ActionResult> Index()
        {
            return View(await db.TaskTypes.ToListAsync());
        }

        // GET: Admin/TaskType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskType taskType = await db.TaskTypes.FindAsync(id);
            if (taskType == null)
            {
                return HttpNotFound();
            }
            return View(taskType);
        }

        // GET: Admin/TaskType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TaskType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] TaskType taskType)
        {
            if (ModelState.IsValid)
            {
                db.TaskTypes.Add(taskType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taskType);
        }

        // GET: Admin/TaskType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskType taskType = await db.TaskTypes.FindAsync(id);
            if (taskType == null)
            {
                return HttpNotFound();
            }
            return View(taskType);
        }

        // POST: Admin/TaskType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] TaskType taskType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskType);
        }

        // GET: Admin/TaskType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskType taskType = await db.TaskTypes.FindAsync(id);
            if (taskType == null)
            {
                return HttpNotFound();
            }
            return View(taskType);
        }

        // POST: Admin/TaskType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaskType taskType = await db.TaskTypes.FindAsync(id);
            db.TaskTypes.Remove(taskType);
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
