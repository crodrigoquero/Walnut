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
    public class JobApplicationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/JobApplication
        public async Task<ActionResult> Index()
        {
            return View(await db.JobApplications.ToListAsync());
        }

        // GET: Admin/JobApplication/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = await db.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }
            return View(jobApplication);
        }

        // GET: Admin/JobApplication/Create
        public ActionResult Create()
        {

            JobApplication model = new JobApplication
            {
                TimeStamp = DateTime.Now,
                JobVacancies = db.JobVacancies.ToList(),
                Candidates = db.Candidates.ToList()
            };

            return View(model);
        }

        // POST: Admin/JobApplication/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CandidateId,TimeStamp,WithdrawalDate,JobVacancyId")] JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                db.JobApplications.Add(jobApplication);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //jobApplication.TimeStamp = DateTime.Now;
            jobApplication.JobVacancies = db.JobVacancies.ToList();
            jobApplication.Candidates = db.Candidates.ToList();

            return View(jobApplication);
        }

        // GET: Admin/JobApplication/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = await db.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }

            jobApplication.JobVacancies = db.JobVacancies.ToList();
            jobApplication.Candidates = db.Candidates.ToList();

            return View(jobApplication);
        }

        // POST: Admin/JobApplication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CandidateId,TimeStamp,WithdrawalDate,JobVacancyId")] JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobApplication).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            jobApplication.JobVacancies = db.JobVacancies.ToList();
            jobApplication.Candidates = db.Candidates.ToList();

            return View(jobApplication);
        }

        // GET: Admin/JobApplication/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplication jobApplication = await db.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return HttpNotFound();
            }

            jobApplication.JobVacancies = db.JobVacancies.ToList();
            jobApplication.Candidates = db.Candidates.ToList();

            return View(jobApplication);
        }

        // POST: Admin/JobApplication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JobApplication jobApplication = await db.JobApplications.FindAsync(id);
            db.JobApplications.Remove(jobApplication);
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
