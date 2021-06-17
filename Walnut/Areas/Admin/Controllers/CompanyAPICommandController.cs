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
    public class CompanyAPICommandController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CompanyAPICommand
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyAPICommands.ToListAsync());
        }

        // GET: Admin/CompanyAPICommand/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPICommand companyAPICommand = await db.CompanyAPICommands.FindAsync(id);
            if (companyAPICommand == null)
            {
                return HttpNotFound();
            }
            return View(companyAPICommand);
        }

        // GET: Admin/CompanyAPICommand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CompanyAPICommand/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyAPIId,APIcmd")] CompanyAPICommand companyAPICommand)
        {
            if (ModelState.IsValid)
            {
                db.CompanyAPICommands.Add(companyAPICommand);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyAPICommand);
        }

        // GET: Admin/CompanyAPICommand/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPICommand companyAPICommand = await db.CompanyAPICommands.FindAsync(id);
            if (companyAPICommand == null)
            {
                return HttpNotFound();
            }
            return View(companyAPICommand);
        }

        // POST: Admin/CompanyAPICommand/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyAPIId,APIcmd")] CompanyAPICommand companyAPICommand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyAPICommand).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyAPICommand);
        }

        // GET: Admin/CompanyAPICommand/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAPICommand companyAPICommand = await db.CompanyAPICommands.FindAsync(id);
            if (companyAPICommand == null)
            {
                return HttpNotFound();
            }
            return View(companyAPICommand);
        }

        // POST: Admin/CompanyAPICommand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyAPICommand companyAPICommand = await db.CompanyAPICommands.FindAsync(id);
            db.CompanyAPICommands.Remove(companyAPICommand);
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
