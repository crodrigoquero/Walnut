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
    public class CompanyDepartmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CompanyDepartment
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyDepartments.OrderBy(x => x.Description).ToListAsync());
        }

        // GET: Admin/CompanyDepartment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDepartment companyDepartment = await db.CompanyDepartments.FindAsync(id);
            if (companyDepartment == null)
            {
                return HttpNotFound();
            }
            return View(companyDepartment);
        }

        // GET: Admin/CompanyDepartment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CompanyDepartment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Explanation")] CompanyDepartment companyDepartment)
        {
            if (ModelState.IsValid)
            {
                db.CompanyDepartments.Add(companyDepartment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyDepartment);
        }

        // GET: Admin/CompanyDepartment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDepartment companyDepartment = await db.CompanyDepartments.FindAsync(id);
            if (companyDepartment == null)
            {
                return HttpNotFound();
            }
            return View(companyDepartment);
        }

        // POST: Admin/CompanyDepartment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Explanation")] CompanyDepartment companyDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyDepartment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyDepartment);
        }

        // GET: Admin/CompanyDepartment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDepartment companyDepartment = await db.CompanyDepartments.FindAsync(id);
            if (companyDepartment == null)
            {
                return HttpNotFound();
            }
            return View(companyDepartment);
        }

        // POST: Admin/CompanyDepartment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyDepartment companyDepartment = await db.CompanyDepartments.FindAsync(id);
            db.CompanyDepartments.Remove(companyDepartment);
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
