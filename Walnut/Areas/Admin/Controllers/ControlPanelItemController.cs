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
    public class ControlPanelItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ControlPanelItem
        public async Task<ActionResult> Index()
        {
            return View(await db.ControlPanelItems.ToListAsync());
        }

        // GET: Admin/ControlPanelItem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlPanelItem controlPanelItem = await db.ControlPanelItems.FindAsync(id);
            if (controlPanelItem == null)
            {
                return HttpNotFound();
            }
            return View(controlPanelItem);
        }

        // GET: Admin/ControlPanelItem/Create
        public ActionResult Create()
        {

            var model = new ControlPanelItem
            {
                ControlPanels =  db.ControlPanels.ToList()
            };

            return View(model);
        }

        // POST: Admin/ControlPanelItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,ControlPanelId,MinValue,MaxValue,Value,On,ProgressBar,Gauge")] ControlPanelItem controlPanelItem)
        {
            if (ModelState.IsValid)
            {
                db.ControlPanelItems.Add(controlPanelItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            controlPanelItem.ControlPanels = db.ControlPanels.ToList();
            return View(controlPanelItem);
        }

        // GET: Admin/ControlPanelItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlPanelItem controlPanelItem = await db.ControlPanelItems.FindAsync(id);
            if (controlPanelItem == null)
            {
                return HttpNotFound();
            }
            controlPanelItem.ControlPanels = db.ControlPanels.ToList();
            return View(controlPanelItem);
        }

        // POST: Admin/ControlPanelItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,ControlPanelId,MinValue,MaxValue,Value,On,ProgressBar,Gauge")] ControlPanelItem controlPanelItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(controlPanelItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            controlPanelItem.ControlPanels = db.ControlPanels.ToList();
            return View(controlPanelItem);
        }

        // GET: Admin/ControlPanelItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlPanelItem controlPanelItem = await db.ControlPanelItems.FindAsync(id);
            if (controlPanelItem == null)
            {
                return HttpNotFound();
            }
            return View(controlPanelItem);
        }

        // POST: Admin/ControlPanelItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ControlPanelItem controlPanelItem = await db.ControlPanelItems.FindAsync(id);
            db.ControlPanelItems.Remove(controlPanelItem);
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
