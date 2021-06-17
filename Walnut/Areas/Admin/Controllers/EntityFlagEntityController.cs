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
    public class EntityFlagEntityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/EntityFlagEntity
        public async Task<ActionResult> Index()
        {
            var model = await db.EntityFlagEntities.ToListAsync();
            return View(model);
        }

        // GET: Admin/EntityFlagEntity/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityFlagEntity entityFlagEntity = await db.EntityFlagEntities.FindAsync(id);
            if (entityFlagEntity == null)
            {
                return HttpNotFound();
            }
            return View(entityFlagEntity);
        }

        // GET: Admin/EntityFlagEntity/Create
        public ActionResult Create()
        {
            var model = new EntityFlagEntity
            {


                EntityFlags =  db.EntityFlags.ToList()


            };
            return View(model);
        }

        // POST: Admin/EntityFlagEntity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EntityId,EntityFlagId")] EntityFlagEntity entityFlagEntity)
        {
            if (ModelState.IsValid)
            {
                db.EntityFlagEntities.Add(entityFlagEntity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(entityFlagEntity);
        }

        // GET: Admin/EntityFlagEntity/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityFlagEntity entityFlagEntity = await db.EntityFlagEntities.FindAsync(id);
            if (entityFlagEntity == null)
            {
                return HttpNotFound();
            }
            entityFlagEntity.EntityFlags = await db.EntityFlags.ToListAsync();

            return View(entityFlagEntity);
        }

        // POST: Admin/EntityFlagEntity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EntityId,EntityFlagId,EntityFlags")] EntityFlagEntity entityFlagEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entityFlagEntity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(entityFlagEntity);
        }

        // GET: Admin/EntityFlagEntity/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityFlagEntity entityFlagEntity = await db.EntityFlagEntities.FindAsync(id);
            if (entityFlagEntity == null)
            {
                return HttpNotFound();
            }
            return View(entityFlagEntity);
        }

        // POST: Admin/EntityFlagEntity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EntityFlagEntity entityFlagEntity = await db.EntityFlagEntities.FindAsync(id);
            db.EntityFlagEntities.Remove(entityFlagEntity);
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
