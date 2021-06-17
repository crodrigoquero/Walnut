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
    public class InterviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Interview
        public async Task<ActionResult> Index()
        {
            return View(await db.Interviews.ToListAsync());
        }

        // GET: Admin/CandidateInterview/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = await db.Interviews.FindAsync(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // GET: Admin/CandidateInterview/Create
        public ActionResult Create()
        {

            var model = new Interview
            {
                Candidates = db.Candidates.ToList(),
                InterviewStartDateAndTime = DateTime.Now

            };
            return View(model);
        }

        // POST: Admin/CandidateInterview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CandidateId,InterviewStartDateAndTime,DurationInMin")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                db.Interviews.Add(interview);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            interview.Candidates = db.Candidates.ToList();
            return View(interview);
        }

        // GET: Admin/CandidateInterview/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = await db.Interviews.FindAsync(id);
            if (interview == null)
            {
                return HttpNotFound();
            }

            interview.Candidates = db.Candidates.ToList();
            return View(interview);
        }

        // POST: Admin/CandidateInterview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CandidateId,InterviewStartDateAndTime,DurationInMin")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interview).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            interview.Candidates = db.Candidates.ToList();
            return View(interview);
        }

        // GET: Admin/CandidateInterview/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = await db.Interviews.FindAsync(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // POST: Admin/CandidateInterview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Interview interview = await db.Interviews.FindAsync(id);
            db.Interviews.Remove(interview);
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
