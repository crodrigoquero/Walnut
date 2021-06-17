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
    public class GenderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Gender
        public async Task<ActionResult> Index()
        {
            return View(await db.Genders.ToListAsync());
        }

        // GET: Admin/Gender/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = await db.Genders.FindAsync(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // GET: Admin/Gender/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Gender/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Genders.Add(gender);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gender);
        }

        // GET: Admin/Gender/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = await db.Genders.FindAsync(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Admin/Gender/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gender).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(gender);
        }

        // GET: Admin/Gender/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = await db.Genders.FindAsync(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Admin/Gender/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Gender gender = await db.Genders.FindAsync(id);
            db.Genders.Remove(gender);
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
