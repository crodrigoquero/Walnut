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
    public class EntityFlagController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/EntityFlag
        public async Task<ActionResult> Index()
        {
            return View(await db.EntityFlags.ToListAsync());
        }

        // GET: Admin/EntityFlag/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityFlag entityFlag = await db.EntityFlags.FindAsync(id);
            if (entityFlag == null)
            {
                return HttpNotFound();
            }
            return View(entityFlag);
        }

        // GET: Admin/EntityFlag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/EntityFlag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,EntityName,EntityPKId,Icon,FlagName,UsageRemarks,Definition,IsCombinable")] EntityFlag entityFlag)
        {
            if (ModelState.IsValid)
            {
                db.EntityFlags.Add(entityFlag);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(entityFlag);
        }

        // GET: Admin/EntityFlag/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityFlag entityFlag = await db.EntityFlags.FindAsync(id);
            if (entityFlag == null)
            {
                return HttpNotFound();
            }
            return View(entityFlag);
        }

        // POST: Admin/EntityFlag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,EntityName,EntityPKId,Icon,FlagName,UsageRemarks,Definition,IsCombinable")] EntityFlag entityFlag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entityFlag).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(entityFlag);
        }

        // GET: Admin/EntityFlag/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityFlag entityFlag = await db.EntityFlags.FindAsync(id);
            if (entityFlag == null)
            {
                return HttpNotFound();
            }
            return View(entityFlag);
        }

        // POST: Admin/EntityFlag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EntityFlag entityFlag = await db.EntityFlags.FindAsync(id);
            db.EntityFlags.Remove(entityFlag);
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
