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
    public class ProcessTaskTemplateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[HttpPost]
        //public ActionResult PreviewWiki(string source)
        //{
        //    return Json(new { RenderedSource = m_wikiEngine.Render(source, GetRenderers()) });
        //}


        // GET: Admin/CandidateFile
        [HttpGet]
        public ActionResult GetProcessStages(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return Json("400", Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
            }

            db.Configuration.ProxyCreationEnabled = false; // to remove loading child entities (otherwise we get an exception)

            var lista = db.ProcessStageTemplates.ToList().Where(stage => stage.ProcessId == id);


            if (lista.Count() == 0)
            {
                return Json("404", Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
            }

            return Json(lista, Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateProccessTaskSequence(int[] ProcessTaskListToReorder)
        {
            if (ProcessTaskListToReorder == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return Json("400", Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
            }

            db.Configuration.ProxyCreationEnabled = false; // to remove loading child entities (otherwise we get an exception)

            var lista = db.ProcessTasksTemplates.ToList();

            if (lista.Count() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int a = 1;
            foreach (int procTaskId in ProcessTaskListToReorder)
            {
                ProcessTaskTemplate proc = db.ProcessTasksTemplates.Find(procTaskId);
                proc.SequenceNumber = a;
                a++;
            }


            db.SaveChanges();


            return new HttpStatusCodeResult(HttpStatusCode.Accepted);

        }

        //[HttpPost]
        [HttpGet]
        public ActionResult IncreaseLevel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }

            db.Configuration.ProxyCreationEnabled = false; // to remove loading child entities (otherwise we get an exception)

            ProcessTaskTemplate proc  = db.ProcessTasksTemplates.Find(id);

            if (proc == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            int previousSequenceNumber = proc.SequenceNumber -1;
            int nextSequenceNumber = proc.SequenceNumber + 1;

            ProcessTaskTemplate previousTask = db.ProcessTasksTemplates.Where(x => x.SequenceNumber == previousSequenceNumber).SingleOrDefault();
            ProcessTaskTemplate nextTask = db.ProcessTasksTemplates.Where(x => x.SequenceNumber == nextSequenceNumber).SingleOrDefault();

            if (previousTask == null) // if has no proevious task we can do nothing
            {
                previousTask = proc;
                previousTask.Level = -1;
            }

            if (nextTask == null) // current task is the last one
            {
                nextTask = proc;
                proc.HasChild = false;
            
            }

            // but if the current task has a previous one (father candidate)...
            proc.ParentTaskId = previousTask.Id;


            // child task Must have higher level than its father task
            proc.Level = previousTask.Level + 1;

            //make sure level increses only once in relation to its father task
            if ((proc.Level + 1) > (previousTask.Level + 2))
            {
                proc.Level = proc.Level - 2;
            }
            else
            {
                proc.Level = previousTask.Level + 1;
            }

            if (nextTask.ParentTaskId == proc.Id) proc.HasChild = true;
                
       
            if (proc.SequenceNumber == 1)
            {
                proc.Level = 0;
                proc.ParentTaskId = 0;

            }

            //rest % completion in all father tasks
            foreach (ProcessTaskTemplate task in db.ProcessTasksTemplates.ToList())
            {
                if (task.HasChild == true) task.PercentComplete = 0;
            }

            db.SaveChanges();

            //find last node of the rows hierachy
            ProcessTaskTemplate lastNode = GetLastHierachyNodeRecursive(proc);

            //calculate acumulative nested task completion percent
            CalculateNestedTaskProgressRecursive(lastNode);

            db.SaveChanges();


            //return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            return Redirect(Request.UrlReferrer.ToString() + "#header");

        }

        /// <summary>
        /// Gets the last hierachy node of a level 0 task
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        private ProcessTaskTemplate GetLastHierachyNodeRecursive(ProcessTaskTemplate proc)
        {
            if (proc.HasChild == false)
            {
                return proc; //terminate recursion
            }


            //find child and father tasks (next one)
            int nextSequenceNumber = proc.SequenceNumber + 1;
            ProcessTaskTemplate chilTask = db.ProcessTasksTemplates.Where(x => x.SequenceNumber == nextSequenceNumber).SingleOrDefault();



            proc = GetLastHierachyNodeRecursive(chilTask);

            if (proc == null)
            {
                return null;
            }

 
            return proc;
        }

        private ProcessTaskTemplate CalculateNestedTaskProgressRecursive(ProcessTaskTemplate proc)
        {
            if (proc.ParentTaskId == 0)
            {
                return null;
            }


            //find parent task and add current task %completion percent
            ProcessTaskTemplate parentTask = db.ProcessTasksTemplates.Where(x => x.Id == proc.ParentTaskId).SingleOrDefault();



            
            parentTask.PercentComplete = proc.PercentComplete + parentTask.PercentComplete;
            db.SaveChanges();

            return CalculateNestedTaskProgressRecursive(parentTask);
        }

        // GET: Admin/ProcessStageTask
        public ActionResult Index(int? ProcessId, int? ProcessStageId)
        {

            // model = await db.ProcessStages.Where(x => x.ProcessId == ProcessId).OrderBy(p => p.SequenceNumber).ToListAsync();

            //return View(await db.ProcessTasksTemplates.OrderBy(x => x.SequenceNumber).ToListAsync());

            var model = new List<ProcessTaskTemplate>();

            if (ProcessId != null && ProcessStageId != null)
            {

                model =  db.ProcessTasksTemplates.Where(x => x.ProcessId == ProcessId && x.ProcessStageId == ProcessStageId).OrderBy(p => p.SequenceNumber).ToList();

                Session["Filtered"] = true;




            }
            else
            {
                Session["Filtered"] = false;
                model = db.ProcessTasksTemplates.OrderBy(p => p.SequenceNumber).ToList();

            }

            if (model.Count == 0)
            {
                return HttpNotFound();
            }

            return View(model);

        }





        // GET: Admin/ProcessStageTask
        public ActionResult EmbeddedList(int? ProcessId, int? ProcessStageId)
        {

            // model = await db.ProcessStages.Where(x => x.ProcessId == ProcessId).OrderBy(p => p.SequenceNumber).ToListAsync();

            //return View(await db.ProcessTasksTemplates.OrderBy(x => x.SequenceNumber).ToListAsync());

            var model = new List<ProcessTaskTemplate>();

            if (ProcessId != null && ProcessStageId != null)
            {

                model = db.ProcessTasksTemplates.Where(x => x.ProcessId == ProcessId && x.ProcessStageId == ProcessStageId).OrderBy(p => p.SequenceNumber).ToList();

                Session["Filtered"] = true;




            }
            else
            {
                Session["Filtered"] = false;
                model = db.ProcessTasksTemplates.OrderBy(p => p.SequenceNumber).ToList();

            }

            if (model.Count == 0)
            {
                return HttpNotFound();
            }

            return PartialView("Index", model);

        }







        // GET: Admin/ProcessStageTask/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.ProcessTaskTemplate processStageTask = await db.ProcessTasksTemplates.FindAsync(id);
            if (processStageTask == null)
            {
                return HttpNotFound();
            }
            return View(processStageTask);
        }

        // GET: Admin/ProcessStageTask/Create
        public ActionResult Create()
        {

            var model = new ProcessTaskTemplate
            {
                Processes = db.ProcessTemplates.ToList(),
                TaskTypes = db.TaskTypes.ToList(),
                ProcessStages = db.ProcessStageTemplates.ToList()

            };
            return View(model);
        }

        // POST: Admin/ProcessStageTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,ProcessId,ProcessStageId,Explanation,TaskTypeId,StartDate,EndDate,PercentComplete,ParentTaskId,SequenceNumber,Level,HasChild")] Entities.ProcessTaskTemplate processStageTask)
        {
            if (ModelState.IsValid)
            {
                db.ProcessTasksTemplates.Add(processStageTask);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            processStageTask.TaskTypes = await db.TaskTypes.ToListAsync();
            processStageTask.Processes = await db.ProcessTemplates.ToListAsync();
            processStageTask.ProcessStages = await db.ProcessStageTemplates.ToListAsync();
            return View(processStageTask);
        }

        // GET: Admin/ProcessStageTask/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.ProcessTaskTemplate processStageTask = await db.ProcessTasksTemplates.FindAsync(id);
            if (processStageTask == null)
            {
                return HttpNotFound();
            }
            processStageTask.TaskTypes = await db.TaskTypes.ToListAsync();
            processStageTask.Processes = await db.ProcessTemplates.ToListAsync();
            processStageTask.ProcessStages = await db.ProcessStageTemplates.ToListAsync();
            return View(processStageTask);

            //return Redirect(Request.UrlReferrer.ToString());
        }

        // POST: Admin/ProcessStageTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,ProcessId,ProcessStageId,Explanation,TaskTypeId,StartDate,EndDate,PercentComplete,ParentTaskId,SequenceNumber,Level,HasChild")] Entities.ProcessTaskTemplate processStageTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processStageTask).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            processStageTask.TaskTypes = await db.TaskTypes.ToListAsync();
            processStageTask.Processes = await db.ProcessTemplates.ToListAsync();
            processStageTask.ProcessStages = await db.ProcessStageTemplates.ToListAsync();
            return View(processStageTask);
        }

        // GET: Admin/ProcessStageTask/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.ProcessTaskTemplate processStageTask = await db.ProcessTasksTemplates.FindAsync(id);
            if (processStageTask == null)
            {
                return HttpNotFound();
            }
            return View(processStageTask);
        }

        // POST: Admin/ProcessStageTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Entities.ProcessTaskTemplate processStageTask = await db.ProcessTasksTemplates.FindAsync(id);
            db.ProcessTasksTemplates.Remove(processStageTask);
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
