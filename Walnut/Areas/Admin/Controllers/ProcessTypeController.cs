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
    public class ProcessTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProcessType
        public async Task<ActionResult> Index()
        {
            return View(await db.ProcessTypes.ToListAsync());
        }

        // GET: Admin/ProcessType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessType processType = await db.ProcessTypes.FindAsync(id);
            if (processType == null)
            {
                return HttpNotFound();
            }
            return View(processType);
        }

        // GET: Admin/ProcessType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProcessType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Explanation")] ProcessType processType)
        {
            if (ModelState.IsValid)
            {
                db.ProcessTypes.Add(processType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(processType);
        }

        // GET: Admin/ProcessType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessType processType = await db.ProcessTypes.FindAsync(id);
            if (processType == null)
            {
                return HttpNotFound();
            }
            return View(processType);
        }

        // POST: Admin/ProcessType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Explanation")] ProcessType processType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(processType);
        }

        // GET: Admin/ProcessType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessType processType = await db.ProcessTypes.FindAsync(id);
            if (processType == null)
            {
                return HttpNotFound();
            }
            return View(processType);
        }

        // POST: Admin/ProcessType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProcessType processType = await db.ProcessTypes.FindAsync(id);
            db.ProcessTypes.Remove(processType);
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
