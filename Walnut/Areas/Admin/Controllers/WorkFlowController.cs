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
    public class WorkFlowController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/WorkFlow
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkFlows.ToListAsync());
        }

        // GET: Admin/WorkFlow/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlow workFlow = await db.WorkFlows.FindAsync(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            return View(workFlow);
        }

        // GET: Admin/WorkFlow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/WorkFlow/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] WorkFlow workFlow)
        {
            if (ModelState.IsValid)
            {
                db.WorkFlows.Add(workFlow);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(workFlow);
        }

        // GET: Admin/WorkFlow/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlow workFlow = await db.WorkFlows.FindAsync(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            return View(workFlow);
        }

        // POST: Admin/WorkFlow/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] WorkFlow workFlow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workFlow).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workFlow);
        }

        // GET: Admin/WorkFlow/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlow workFlow = await db.WorkFlows.FindAsync(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            return View(workFlow);
        }

        // POST: Admin/WorkFlow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkFlow workFlow = await db.WorkFlows.FindAsync(id);
            db.WorkFlows.Remove(workFlow);
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
