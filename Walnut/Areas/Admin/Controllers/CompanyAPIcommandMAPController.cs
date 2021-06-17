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
    public class CompanyAPIcommandMAPController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CompanyAPIcommandMAP
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyAPIcommandMAPs.ToListAsync());
        }

        // GET: Admin/CompanyAPIcommandMAP/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPIcommandMAP companyAPIcommandMAP = await db.CompanyAPIcommandMAPs.FindAsync(id);
            if (companyAPIcommandMAP == null)
            {
                return HttpNotFound();
            }
            return View(companyAPIcommandMAP);
        }

        // GET: Admin/CompanyAPIcommandMAP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CompanyAPIcommandMAP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyAPICommandId,urlParameterName,urlParameterValue_EntityName,urlParameterValue_EntityRecordId,urlParameterValue_EntityPropertyName")] CompanyAPIcommandMAP companyAPIcommandMAP)
        {
            if (ModelState.IsValid)
            {
                db.CompanyAPIcommandMAPs.Add(companyAPIcommandMAP);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyAPIcommandMAP);
        }

        // GET: Admin/CompanyAPIcommandMAP/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPIcommandMAP companyAPIcommandMAP = await db.CompanyAPIcommandMAPs.FindAsync(id);
            if (companyAPIcommandMAP == null)
            {
                return HttpNotFound();
            }
            return View(companyAPIcommandMAP);
        }

        // POST: Admin/CompanyAPIcommandMAP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyAPICommandId,urlParameterName,urlParameterValue_EntityName,urlParameterValue_EntityRecordId,urlParameterValue_EntityPropertyName")] CompanyAPIcommandMAP companyAPIcommandMAP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyAPIcommandMAP).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyAPIcommandMAP);
        }

        // GET: Admin/CompanyAPIcommandMAP/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPIcommandMAP companyAPIcommandMAP = await db.CompanyAPIcommandMAPs.FindAsync(id);
            if (companyAPIcommandMAP == null)
            {
                return HttpNotFound();
            }
            return View(companyAPIcommandMAP);
        }

        // POST: Admin/CompanyAPIcommandMAP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyAPIcommandMAP companyAPIcommandMAP = await db.CompanyAPIcommandMAPs.FindAsync(id);
            db.CompanyAPIcommandMAPs.Remove(companyAPIcommandMAP);
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
