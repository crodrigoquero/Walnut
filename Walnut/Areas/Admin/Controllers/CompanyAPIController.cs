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
    public class CompanyAPIController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CompanyAPI
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyAPIs.ToListAsync());
        }

        // GET: Admin/CompanyAPI/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPI companyAPI = await db.CompanyAPIs.FindAsync(id);
            if (companyAPI == null)
            {
                return HttpNotFound();
            }
            return View(companyAPI);
        }

        // GET: Admin/CompanyAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CompanyAPI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,CompanyId,Explanation,APIurl,APIID")] CompanyAPI companyAPI)
        {
            if (ModelState.IsValid)
            {
                db.CompanyAPIs.Add(companyAPI);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyAPI);
        }

        // GET: Admin/CompanyAPI/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPI companyAPI = await db.CompanyAPIs.FindAsync(id);
            if (companyAPI == null)
            {
                return HttpNotFound();
            }
            return View(companyAPI);
        }

        // POST: Admin/CompanyAPI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,CompanyId,Explanation,APIurl,APIID")] CompanyAPI companyAPI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyAPI).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyAPI);
        }

        // GET: Admin/CompanyAPI/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPI companyAPI = await db.CompanyAPIs.FindAsync(id);
            if (companyAPI == null)
            {
                return HttpNotFound();
            }
            return View(companyAPI);
        }

        // POST: Admin/CompanyAPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyAPI companyAPI = await db.CompanyAPIs.FindAsync(id);
            db.CompanyAPIs.Remove(companyAPI);
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
