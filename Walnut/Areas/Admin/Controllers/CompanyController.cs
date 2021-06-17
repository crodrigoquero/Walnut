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
    public class CompanyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Company
        public async Task<ActionResult> Index()
        {
            return View(await db.Companies.OrderBy(x => x.CompanyName).ToListAsync());
        }

        // GET: Admin/Company/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Admin/Company/Create
        public ActionResult Create()
        {

            var model = new Company
            {
                CompanyTypes =  db.CompanyTypes.ToList(),
                RelationshipTypes = db.RelationshipTypes.ToList()
            };

            return View(model);
        }

        // POST: Admin/Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyName,CompanyTypeId,AddressLine1,AddressLine2,AddressLine3,County,Postcode,CountryId,MainPhoneNumber,URL,LinkedIn,Twitter,Facebook,TimeStamp,RelationshipTypeId")] Company company)
        {
            company.TimeStamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            company.CompanyTypes = db.CompanyTypes.ToList();
            company.RelationshipTypes = db.RelationshipTypes.ToList();

            return View(company);
        }

        // GET: Admin/Company/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);


            if (company == null)
            {
                return HttpNotFound();
            }

            company.CompanyTypes = db.CompanyTypes.ToList();
            company.RelationshipTypes = db.RelationshipTypes.ToList();

            return View(company);
        }

        // POST: Admin/Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyName,CompanyTypeId,AddressLine1,AddressLine2,AddressLine3,County,Postcode,CountryId,MainPhoneNumber,URL,LinkedIn,Twitter,Facebook,TimeStamp,RelationshipTypeId")] Company company)
        {
            company.TimeStamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            company.CompanyTypes = db.CompanyTypes.ToList();
            company.RelationshipTypes = db.RelationshipTypes.ToList();

            return View(company);
        }

        // GET: Admin/Company/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Admin/Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Company company = await db.Companies.FindAsync(id);
            db.Companies.Remove(company);
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
