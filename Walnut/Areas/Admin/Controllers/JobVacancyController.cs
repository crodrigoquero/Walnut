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
    public class JobVacancyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/JobVacancy
        public async Task<ActionResult> Index()
        {
            return View(await db.JobVacancies.ToListAsync());
        }

        // GET: Admin/JobVacancy/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobVacancy jobVacancy = await db.JobVacancies.FindAsync(id);
            if (jobVacancy == null)
            {
                return HttpNotFound();
            }
            return View(jobVacancy);
        }

        // GET: Admin/JobVacancy/Create
        public ActionResult Create()
        {

            JobVacancy model = new JobVacancy
            {
                CompanyDepartments = db.CompanyDepartments.ToList(),
                CompanyContactRoles = db.CompanyContactRoles.ToList(),
                TimeStamp = DateTime.Now
                
            };

            return View(model);

        }

        // POST: Admin/JobVacancy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,TimeStamp,StartDate,EndDate,CompanyDepartmentId,CompanyContactRoleId,ArticleId")] JobVacancy jobVacancy)
        {
            if (ModelState.IsValid)
            {
                jobVacancy.TimeStamp = DateTime.Now;
                db.JobVacancies.Add(jobVacancy);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            jobVacancy.CompanyDepartments = db.CompanyDepartments.ToList();
            jobVacancy.CompanyContactRoles = db.CompanyContactRoles.ToList();

            return View(jobVacancy);
        }

        // GET: Admin/JobVacancy/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobVacancy jobVacancy = await db.JobVacancies.FindAsync(id);
            if (jobVacancy == null)
            {
                return HttpNotFound();
            }

            jobVacancy.CompanyDepartments = db.CompanyDepartments.ToList();
            jobVacancy.CompanyContactRoles = db.CompanyContactRoles.ToList();

            return View(jobVacancy);
        }

        // POST: Admin/JobVacancy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,TimeStamp,StartDate,EndDate,CompanyDepartmentId,CompanyContactRoleId,ArticleId")] JobVacancy jobVacancy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobVacancy).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            jobVacancy.CompanyDepartments = db.CompanyDepartments.ToList();
            jobVacancy.CompanyContactRoles = db.CompanyContactRoles.ToList();

            return View(jobVacancy);
        }

        // GET: Admin/JobVacancy/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobVacancy jobVacancy = await db.JobVacancies.FindAsync(id);
            if (jobVacancy == null)
            {
                return HttpNotFound();
            }

            jobVacancy.CompanyDepartments = db.CompanyDepartments.ToList();
            jobVacancy.CompanyContactRoles = db.CompanyContactRoles.ToList();

            return View(jobVacancy);
        }

        // POST: Admin/JobVacancy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JobVacancy jobVacancy = await db.JobVacancies.FindAsync(id);
            db.JobVacancies.Remove(jobVacancy);
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
