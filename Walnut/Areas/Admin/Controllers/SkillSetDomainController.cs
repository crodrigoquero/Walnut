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
    public class SkillSetDomainController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SkillSetDomain
        public async Task<ActionResult> Index()
        {
            return View(await db.SkillSetDomains.ToListAsync());
        }

        // GET: Admin/SkillSetDomain/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillSetDomain skillSetDomain = await db.SkillSetDomains.FindAsync(id);
            if (skillSetDomain == null)
            {
                return HttpNotFound();
            }
            return View(skillSetDomain);
        }

        // GET: Admin/SkillSetDomain/Create
        public ActionResult Create()
        {
            var model = new SkillSetDomain
            {
                CompanyTypes = db.CompanyTypes.ToList(),
                CompanyDepartments = db.CompanyDepartments.ToList()

            };

            return View(model);
        }

        // POST: Admin/SkillSetDomain/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,CompanyTypeId,CompanyDepartmentId")] SkillSetDomain skillSetDomain)
        {
            if (ModelState.IsValid)
            {
                db.SkillSetDomains.Add(skillSetDomain);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            skillSetDomain.CompanyTypes = db.CompanyTypes.ToList();
            skillSetDomain.CompanyDepartments = db.CompanyDepartments.ToList();

            return View(skillSetDomain);
        }

        // GET: Admin/SkillSetDomain/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillSetDomain skillSetDomain = await db.SkillSetDomains.FindAsync(id);
            if (skillSetDomain == null)
            {
                return HttpNotFound();
            }

            skillSetDomain.CompanyTypes = db.CompanyTypes.ToList();
            skillSetDomain.CompanyDepartments = db.CompanyDepartments.ToList();

            return View(skillSetDomain);
        }

        // POST: Admin/SkillSetDomain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,CompanyTypeId,CompanyDepartmentId")] SkillSetDomain skillSetDomain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillSetDomain).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            skillSetDomain.CompanyTypes = db.CompanyTypes.ToList();
            skillSetDomain.CompanyDepartments = db.CompanyDepartments.ToList();

            return View(skillSetDomain);
        }

        // GET: Admin/SkillSetDomain/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillSetDomain skillSetDomain = await db.SkillSetDomains.FindAsync(id);
            if (skillSetDomain == null)
            {
                return HttpNotFound();
            }
            return View(skillSetDomain);
        }

        // POST: Admin/SkillSetDomain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SkillSetDomain skillSetDomain = await db.SkillSetDomains.FindAsync(id);
            db.SkillSetDomains.Remove(skillSetDomain);
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
