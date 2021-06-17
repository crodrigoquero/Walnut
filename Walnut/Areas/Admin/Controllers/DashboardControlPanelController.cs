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
    public class DashboardControlPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/DashboardControlPanel
        public async Task<ActionResult> Index()
        {
            return View(await db.DashboardControlPanels.ToListAsync());
        }

        // GET: Admin/DashboardControlPanel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardControlPanel dashboardControlPanel = await db.DashboardControlPanels.FindAsync(id);
            if (dashboardControlPanel == null)
            {
                return HttpNotFound();
            }
            return View(dashboardControlPanel);
        }

        // GET: Admin/DashboardControlPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DashboardControlPanel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DasboardId,ControlPanelId")] DashboardControlPanel dashboardControlPanel)
        {
            if (ModelState.IsValid)
            {
                db.DashboardControlPanels.Add(dashboardControlPanel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(dashboardControlPanel);
        }

        // GET: Admin/DashboardControlPanel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardControlPanel dashboardControlPanel = await db.DashboardControlPanels.FindAsync(id);
            if (dashboardControlPanel == null)
            {
                return HttpNotFound();
            }
            return View(dashboardControlPanel);
        }

        // POST: Admin/DashboardControlPanel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DasboardId,ControlPanelId")] DashboardControlPanel dashboardControlPanel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dashboardControlPanel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dashboardControlPanel);
        }

        // GET: Admin/DashboardControlPanel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardControlPanel dashboardControlPanel = await db.DashboardControlPanels.FindAsync(id);
            if (dashboardControlPanel == null)
            {
                return HttpNotFound();
            }
            return View(dashboardControlPanel);
        }

        // POST: Admin/DashboardControlPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DashboardControlPanel dashboardControlPanel = await db.DashboardControlPanels.FindAsync(id);
            db.DashboardControlPanels.Remove(dashboardControlPanel);
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
