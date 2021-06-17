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
    public class RelationshipTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/RelationshipType
        public async Task<ActionResult> Index()
        {
            return View(await db.RelationshipTypes.OrderBy(x => x.Description).ToListAsync());
        }

        // GET: Admin/RelationshipType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelationshipType relationshipType = await db.RelationshipTypes.FindAsync(id);
            if (relationshipType == null)
            {
                return HttpNotFound();
            }
            return View(relationshipType);
        }

        // GET: Admin/RelationshipType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RelationshipType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] RelationshipType relationshipType)
        {
            if (ModelState.IsValid)
            {
                db.RelationshipTypes.Add(relationshipType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(relationshipType);
        }

        // GET: Admin/RelationshipType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelationshipType relationshipType = await db.RelationshipTypes.FindAsync(id);
            if (relationshipType == null)
            {
                return HttpNotFound();
            }
            return View(relationshipType);
        }

        // POST: Admin/RelationshipType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] RelationshipType relationshipType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relationshipType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(relationshipType);
        }

        // GET: Admin/RelationshipType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelationshipType relationshipType = await db.RelationshipTypes.FindAsync(id);
            if (relationshipType == null)
            {
                return HttpNotFound();
            }
            return View(relationshipType);
        }

        // POST: Admin/RelationshipType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RelationshipType relationshipType = await db.RelationshipTypes.FindAsync(id);
            db.RelationshipTypes.Remove(relationshipType);
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
