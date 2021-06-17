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
    public class SkillTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SkillType
        public async Task<ActionResult> Index()
        {
            return View(await db.SkillTypes.ToListAsync());
        }

        // GET: Admin/SkillType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillType skillType = await db.SkillTypes.FindAsync(id);
            if (skillType == null)
            {
                return HttpNotFound();
            }
            return View(skillType);
        }

        // GET: Admin/SkillType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SkillType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] SkillType skillType)
        {
            if (ModelState.IsValid)
            {
                db.SkillTypes.Add(skillType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(skillType);
        }

        // GET: Admin/SkillType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillType skillType = await db.SkillTypes.FindAsync(id);
            if (skillType == null)
            {
                return HttpNotFound();
            }
            return View(skillType);
        }

        // POST: Admin/SkillType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] SkillType skillType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(skillType);
        }

        // GET: Admin/SkillType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillType skillType = await db.SkillTypes.FindAsync(id);
            if (skillType == null)
            {
                return HttpNotFound();
            }
            return View(skillType);
        }

        // POST: Admin/SkillType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SkillType skillType = await db.SkillTypes.FindAsync(id);
            db.SkillTypes.Remove(skillType);
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
