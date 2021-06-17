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
    public class SkillController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Skill
        public async Task<ActionResult> Index()
        {
            return View(await db.Skills.ToListAsync());
        }

        // GET: Admin/Skill/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // GET: Admin/Skill/Create
        public ActionResult Create()
        {

            var model = new Skill
            {
                SkillSets = db.SkillSets.OrderBy(x => x.Description).ToList(),
                SkillSetDomains = db.SkillSetDomains.OrderBy(x => x.Description).ToList(),
                SkillTypes = db.SkillTypes.OrderBy(x => x.Description).ToList()
                
            };

            return View(model);
        }

        // POST: Admin/Skill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,SkillSetId,SkillDomainId,SkillTypeId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Skills.Add(skill);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            skill.SkillSets = db.SkillSets.OrderBy(x => x.Description).ToList();
            skill.SkillSetDomains = db.SkillSetDomains.OrderBy(x => x.Description).ToList();
            skill.SkillTypes = db.SkillTypes.OrderBy(x => x.Description).ToList();

            return View(skill);
        }

        // GET: Admin/Skill/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return HttpNotFound();
            }

            skill.SkillSets = db.SkillSets.OrderBy(x => x.Description).ToList();
            skill.SkillSetDomains = db.SkillSetDomains.OrderBy(x => x.Description).ToList();
            skill.SkillTypes = db.SkillTypes.OrderBy(x => x.Description).ToList();

            return View(skill);
        }

        // POST: Admin/Skill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,SkillSetId,SkillDomainId,SkillTypeId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            skill.SkillSets = db.SkillSets.OrderBy(x => x.Description).ToList();
            skill.SkillSetDomains = db.SkillSetDomains.OrderBy(x => x.Description).ToList();
            skill.SkillTypes = db.SkillTypes.OrderBy(x => x.Description).ToList();

            return View(skill);
        }

        // GET: Admin/Skill/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Admin/Skill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);
            db.Skills.Remove(skill);
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
