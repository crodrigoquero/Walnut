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
    public class JobVacancySkillSetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/JobVacancySkillSet
        public async Task<ActionResult> Index()
        {
            return View(await db.JobVacancySkillSets.ToListAsync());
        }

        // GET: Admin/JobVacancySkillSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //JobVacancySkillSet jobVacancySkillSet = await db.JobVacancySkillSets.FindAsync(id);
            JobVacancySkillSet jobVacancySkillSet = db.JobVacancySkillSets.SingleOrDefault(m => m.Id == id);

            if (jobVacancySkillSet == null)
            {
                return HttpNotFound();
            }
            return View(jobVacancySkillSet);
        }

        // GET: Admin/JobVacancySkillSet/Create
        public ActionResult Create()
        {

            JobVacancySkillSet model = new JobVacancySkillSet
            {
                SkillSets = db.SkillSets.ToList(),
                JobVacancies = db.JobVacancies.ToList()
            };

            return View(model);
        }

        // POST: Admin/JobVacancySkillSet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,JobVacancyId,SkillSetId")] JobVacancySkillSet jobVacancySkillSet)
        {
            if (ModelState.IsValid)
            {
                db.JobVacancySkillSets.Add(jobVacancySkillSet);

                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");

                } catch (Exception ex)
                {
                    var sqlException = ex.InnerException as System.Data.SqlClient.SqlException;
                    ModelState.AddModelError("SkillSetId", "This relationship already exist ");
                }
                
            }

            jobVacancySkillSet.SkillSets = db.SkillSets.ToList();
            jobVacancySkillSet.JobVacancies = db.JobVacancies.ToList();

            return View(jobVacancySkillSet);
        }

        // GET: Admin/JobVacancySkillSet/Edit/5
        //public  ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //JobVacancySkillSet jobVacancySkillSet = await db.JobVacancySkillSets.FindAsync(id);
        //    // to avoid the error "The number of primary key values passed must match number of primary key values defined on the entity."
        //    // use the below code line:
        //    JobVacancySkillSet jobVacancySkillSet = db.JobVacancySkillSets.SingleOrDefault(m => m.Id == id);
        //    if (jobVacancySkillSet == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(jobVacancySkillSet);
        //}

        // POST: Admin/JobVacancySkillSet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,JobVacancyId,SkillSetId")] JobVacancySkillSet jobVacancySkillSet)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(jobVacancySkillSet).State = EntityState.Modified;

        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(jobVacancySkillSet);
        //}

        // GET: Admin/JobVacancySkillSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //JobVacancySkillSet jobVacancySkillSet = await db.JobVacancySkillSets.FindAsync(id);

            JobVacancySkillSet jobVacancySkillSet =  db.JobVacancySkillSets.SingleOrDefault(m => m.Id == id);
            if (jobVacancySkillSet == null)
            {
                return HttpNotFound();
            }
            return View(jobVacancySkillSet);
        }

        // POST: Admin/JobVacancySkillSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //JobVacancySkillSet jobVacancySkillSet = await db.JobVacancySkillSets.FindAsync(id);
            JobVacancySkillSet jobVacancySkillSet = db.JobVacancySkillSets.SingleOrDefault(m => m.Id == id);
            db.JobVacancySkillSets.Remove(jobVacancySkillSet);
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
