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
    public class CompanyContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CompanyContact
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyContacts.ToListAsync());
        }

        // GET: Admin/CompanyContact/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyContact companyContact = await db.CompanyContacts.FindAsync(id);
            if (companyContact == null)
            {
                return HttpNotFound();
            }
            return View(companyContact);
        }

        // GET: Admin/CompanyContact/Create
        public ActionResult Create()
        {

            var model = new CompanyContact
            {
                Companies = db.Companies.ToList(),
                CompanyDepartments = db.CompanyDepartments.ToList(),
                CompanyContactRoles = db.CompanyContactRoles.ToList()
                
            };

            return View(model);
        }

        // POST: Admin/CompanyContact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyId,CompanyContactRoleId,Department,FirstName,LastName,PhoneNumber,PhoneExtensionNumber,email,CompanyDepartmentId")] CompanyContact companyContact)
        {
            if (ModelState.IsValid)
            {
                db.CompanyContacts.Add(companyContact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            companyContact.Companies = db.Companies.ToList();
            companyContact.CompanyDepartments = db.CompanyDepartments.OrderBy(x => x.Description).ToList();
            companyContact.CompanyContactRoles = db.CompanyContactRoles.OrderBy(x => x.Description).ToList();

            return View(companyContact);
        }

        // GET: Admin/CompanyContact/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyContact companyContact = await db.CompanyContacts.FindAsync(id);
            if (companyContact == null)
            {
                return HttpNotFound();
            }

            companyContact.Companies = db.Companies.ToList();
            companyContact.CompanyDepartments = db.CompanyDepartments.OrderBy(x => x.Description).ToList();
            companyContact.CompanyContactRoles = db.CompanyContactRoles.OrderBy(x => x.Description).ToList();

            return View(companyContact);
        }

        // POST: Admin/CompanyContact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyId,CompanyContactRoleId,Department,FirstName,LastName,PhoneNumber,PhoneExtensionNumber,email,CompanyDepartmentId")] CompanyContact companyContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyContact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            companyContact.Companies = db.Companies.ToList();
            companyContact.CompanyDepartments = db.CompanyDepartments.OrderBy(x => x.Description).ToList();
            companyContact.CompanyContactRoles = db.CompanyContactRoles.OrderBy(x => x.Description).ToList();

            return View(companyContact);
        }

        // GET: Admin/CompanyContact/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyContact companyContact = await db.CompanyContacts.FindAsync(id);
            if (companyContact == null)
            {
                return HttpNotFound();
            }
            return View(companyContact);
        }

        // POST: Admin/CompanyContact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyContact companyContact = await db.CompanyContacts.FindAsync(id);
            db.CompanyContacts.Remove(companyContact);
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
