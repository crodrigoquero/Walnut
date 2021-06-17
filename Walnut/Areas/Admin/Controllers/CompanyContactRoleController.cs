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
    public class CompanyContactRoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CompanyContactRole
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyContactRoles.OrderBy(x => x.Description).ToListAsync());
        }

        // GET: Admin/CompanyContactRole/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyContactRole companyContactRole = await db.CompanyContactRoles.FindAsync(id);
            if (companyContactRole == null)
            {
                return HttpNotFound();
            }
            return View(companyContactRole);
        }

        // GET: Admin/CompanyContactRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CompanyContactRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] CompanyContactRole companyContactRole)
        {
            if (ModelState.IsValid)
            {
                db.CompanyContactRoles.Add(companyContactRole);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyContactRole);
        }

        // GET: Admin/CompanyContactRole/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyContactRole companyContactRole = await db.CompanyContactRoles.FindAsync(id);
            if (companyContactRole == null)
            {
                return HttpNotFound();
            }
            return View(companyContactRole);
        }

        // POST: Admin/CompanyContactRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] CompanyContactRole companyContactRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyContactRole).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyContactRole);
        }

        // GET: Admin/CompanyContactRole/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyContactRole companyContactRole = await db.CompanyContactRoles.FindAsync(id);
            if (companyContactRole == null)
            {
                return HttpNotFound();
            }
            return View(companyContactRole);
        }

        // POST: Admin/CompanyContactRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyContactRole companyContactRole = await db.CompanyContactRoles.FindAsync(id);
            db.CompanyContactRoles.Remove(companyContactRole);
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
