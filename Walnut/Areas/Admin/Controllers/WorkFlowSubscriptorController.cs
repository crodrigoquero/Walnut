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
    public class WorkFlowSubscriptorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/WorkFlowSubscriptor
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkFlowSubscriptors.ToListAsync());
        }

        // GET: Admin/WorkFlowSubscriptor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowSubscriptor workFlowSubscriptor = await db.WorkFlowSubscriptors.FindAsync(id);
            if (workFlowSubscriptor == null)
            {
                return HttpNotFound();
            }
            return View(workFlowSubscriptor);
        }

        // GET: Admin/WorkFlowSubscriptor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/WorkFlowSubscriptor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserId,RoleId")] WorkFlowSubscriptor workFlowSubscriptor)
        {
            if (ModelState.IsValid)
            {
                db.WorkFlowSubscriptors.Add(workFlowSubscriptor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(workFlowSubscriptor);
        }

        // GET: Admin/WorkFlowSubscriptor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowSubscriptor workFlowSubscriptor = await db.WorkFlowSubscriptors.FindAsync(id);
            if (workFlowSubscriptor == null)
            {
                return HttpNotFound();
            }
            return View(workFlowSubscriptor);
        }

        // POST: Admin/WorkFlowSubscriptor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserId,RoleId")] WorkFlowSubscriptor workFlowSubscriptor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workFlowSubscriptor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workFlowSubscriptor);
        }

        // GET: Admin/WorkFlowSubscriptor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlowSubscriptor workFlowSubscriptor = await db.WorkFlowSubscriptors.FindAsync(id);
            if (workFlowSubscriptor == null)
            {
                return HttpNotFound();
            }
            return View(workFlowSubscriptor);
        }

        // POST: Admin/WorkFlowSubscriptor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkFlowSubscriptor workFlowSubscriptor = await db.WorkFlowSubscriptors.FindAsync(id);
            db.WorkFlowSubscriptors.Remove(workFlowSubscriptor);
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
