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
    public class ProcessTemplateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult UpdateProccesssesSequence(int[] ProcessListToReorder)
        {
            if (ProcessListToReorder == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return Json("400", Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
            }

            db.Configuration.ProxyCreationEnabled = false; // to remove loading child entities (otherwise we get an exception)

            var lista = db.ProcessTemplates.ToList();

            if (lista.Count() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int a = 1;
            foreach (int procId in ProcessListToReorder)
            {
                ProcessTemplate proc = db.ProcessTemplates.Find(procId);
                proc.SequenceNumber = a;
                a++;
            }

        
            db.SaveChanges();


            return new HttpStatusCodeResult(HttpStatusCode.Accepted);

        }

        // GET: Admin/Process
        public ActionResult Index()
        {
            return View(db.ProcessTemplates.OrderBy(p => p.SequenceNumber).ToList());
        }





        public ActionResult EmbeddedList()
        {
            return PartialView("Index", db.ProcessTemplates.OrderBy(p => p.SequenceNumber).ToList());
        }





        // GET: Admin/Process/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTemplate process = await db.ProcessTemplates.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        // GET: Admin/Process/Create
        public ActionResult Create()
        {
            var model = new ProcessTemplate
            {
                ProcessTypes = db.ProcessTypes.ToList(),
                Periodicities = db.Periodicities.ToList()
            };

            return View(model);
        }

        // POST: Admin/Process/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,ProcessTypeId,Explanation,SequenceNumber,PeriodicityId")] ProcessTemplate process)
        {
            if (ModelState.IsValid)
            {
                db.ProcessTemplates.Add(process);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            process.ProcessTypes = await db.ProcessTypes.ToListAsync();
            process.Periodicities = await db.Periodicities.ToListAsync();

            return View(process);
        }

        // GET: Admin/Process/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTemplate process = await db.ProcessTemplates.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            process.ProcessTypes = await db.ProcessTypes.ToListAsync();
            process.Periodicities = await db.Periodicities.ToListAsync();

            return View(process);
        }

        // POST: Admin/Process/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,ProcessTypeId,Explanation,SequenceNumber,PeriodicityId")] ProcessTemplate process)
        {
            if (ModelState.IsValid)
            {
                db.Entry(process).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            process.ProcessTypes = await db.ProcessTypes.ToListAsync();
            process.Periodicities = await db.Periodicities.ToListAsync();

            return View(process);
        }

        // GET: Admin/Process/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTemplate process = await db.ProcessTemplates.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        // POST: Admin/Process/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProcessTemplate process = await db.ProcessTemplates.FindAsync(id);
            db.ProcessTemplates.Remove(process);
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
