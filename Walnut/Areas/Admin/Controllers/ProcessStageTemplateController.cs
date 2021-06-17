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
    public class ProcessStageTemplateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult UpdateProccessStagesSequence(int[] ProcessStageListToReorder)
        {
            if (ProcessStageListToReorder == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return Json("400", Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
            }

            db.Configuration.ProxyCreationEnabled = false; // to remove loading child entities (otherwise we get an exception)

            // order only the stages related to a given process!!!
            //var lista = db.ProcessStages.Where(x => x.ProcessId == ProcessId).OrderBy(p => p.SequenceNumber).ToListAsync())
            var lista = db.ProcessStageTemplates.ToList();

            if (lista.Count() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int a = 1;
            foreach (int procStageId in ProcessStageListToReorder)
            {
                ProcessStageTemplate procStage = db.ProcessStageTemplates.Find(procStageId);
                procStage.SequenceNumber = a;
                a++;
            }

            db.SaveChanges();


            return new HttpStatusCodeResult(HttpStatusCode.Accepted);

        }


        // GET: Admin/ProcessStage
        public async Task<ActionResult> Index(int? ProcessId)
        {
            var model = new List<ProcessStageTemplate>();

            if (ProcessId != null)
            {

                model = await db.ProcessStageTemplates.Where(x => x.ProcessId == ProcessId).OrderBy(p => p.SequenceNumber).ToListAsync();

                Session["Filtered"] = true;

                //if (model == null)
                //{
                //    return HttpNotFound();
                //}


            } else
            {
                Session["Filtered"] = false;
                model = await db.ProcessStageTemplates.OrderBy(p => p.SequenceNumber).ToListAsync();


            }

            if (model.Count == 0)
            {
                return HttpNotFound();

            }

            return View(model);

        }

        // GET: Admin/ProcessStage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStageTemplate processStageTemplate = await db.ProcessStageTemplates.FindAsync(id);
            if (processStageTemplate == null)
            {
                return HttpNotFound();
            }
            return View(processStageTemplate);
        }

        // GET: Admin/ProcessStage/Create
        public ActionResult Create()
        {
            var model = new ProcessStageTemplate
            {
                Processes = db.ProcessTemplates.ToList()
            };

            return View(model);
        }

        // POST: Admin/ProcessStage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Explanation,SequenceNumber,ProcessId")] ProcessStageTemplate processStage)
        {
            if (ModelState.IsValid)
            {
                db.ProcessStageTemplates.Add(processStage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            processStage.Processes = db.ProcessTemplates.ToList();
            return View(processStage);
        }

        // GET: Admin/ProcessStage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStageTemplate processStageTemplate = await db.ProcessStageTemplates.FindAsync(id);
            if (processStageTemplate == null)
            {
                return HttpNotFound();
            }

            processStageTemplate.Processes = db.ProcessTemplates.ToList();
            return View(processStageTemplate);
        }

        // POST: Admin/ProcessStage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Explanation,SequenceNumber,ProcessId")] ProcessStageTemplate processStage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processStage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            processStage.Processes = db.ProcessTemplates.ToList();
            return View(processStage);
        }

        // GET: Admin/ProcessStage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStageTemplate processStageTemplate = await db.ProcessStageTemplates.FindAsync(id);
            if (processStageTemplate == null)
            {
                return HttpNotFound();
            }
            return View(processStageTemplate);
        }

        // POST: Admin/ProcessStage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProcessStageTemplate processStageTemplate = await db.ProcessStageTemplates.FindAsync(id);
            db.ProcessStageTemplates.Remove(processStageTemplate);
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
