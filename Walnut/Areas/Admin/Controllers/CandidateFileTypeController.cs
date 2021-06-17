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
    public class CandidateFileTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CandidateFileType
        public async Task<ActionResult> Index()
        {
            return View(await db.CandidateFileTypes.ToListAsync());
        }

        // GET: Admin/CandidateFileType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateFileType candidateFileType = await db.CandidateFileTypes.FindAsync(id);
            if (candidateFileType == null)
            {
                return HttpNotFound();
            }
            return View(candidateFileType);
        }

        // GET: Admin/CandidateFileType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CandidateFileType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] CandidateFileType candidateFileType)
        {
            if (ModelState.IsValid)
            {
                db.CandidateFileTypes.Add(candidateFileType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(candidateFileType);
        }

        // GET: Admin/CandidateFileType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateFileType candidateFileType = await db.CandidateFileTypes.FindAsync(id);
            if (candidateFileType == null)
            {
                return HttpNotFound();
            }
            return View(candidateFileType);
        }

        // POST: Admin/CandidateFileType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] CandidateFileType candidateFileType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateFileType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(candidateFileType);
        }

        // GET: Admin/CandidateFileType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateFileType candidateFileType = await db.CandidateFileTypes.FindAsync(id);
            if (candidateFileType == null)
            {
                return HttpNotFound();
            }
            return View(candidateFileType);
        }

        // POST: Admin/CandidateFileType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CandidateFileType candidateFileType = await db.CandidateFileTypes.FindAsync(id);
            db.CandidateFileTypes.Remove(candidateFileType);
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
