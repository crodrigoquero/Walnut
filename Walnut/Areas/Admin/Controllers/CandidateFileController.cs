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
using System.IO;
using System.Web.Routing;

namespace Walnut.Areas.Admin.Controllers
{
    public class CandidateFileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CandidateFile
        public ActionResult Index(int? candidateId)
        {


            if (candidateId != null)
            {
                var items = from item in db.CandidateFiles.Include(c => c.Candidate)
                                                   where item.CandidateId == candidateId
                            select item;

                if (items == null || items.Count() == 0)
                {
                    return HttpNotFound();
                }

                Session["Filtered"] = true; // we are dealing just with a given candidate data
                //@Session["candidateDescription"] = items.ToList().Find(c => c.CandidateId == candidateId).Description;
                return View(items.ToList());
            } else
            {
                var candateFile = db.CandidateFiles.Include(c => c.Candidate);
                Session["Filtered"] = false;
                return View(candateFile.ToList());
            }

            
        }

        // GET: Admin/CandidateFile
        //[HttpGet]
        //public  ActionResult ToJSON()
        //{
        //    //if (id == null)
        //    //{
        //    //   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //   return Json("400", Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
        //    //}

        //    db.Configuration.ProxyCreationEnabled = false; // to remove loading child entities (otherwise we get an exception)

        //    List<CandidateFile> lista = db.CandidateFiles.ToList();
        //    IEnumerable<CandidateFile> items = from item in lista
        //                 where item.Id == 1
        //                 select new CandidateFile
        //                    {
        //                        Id = item.Id,
        //                        Description = item.Description,
        //                        FileName = item.FileName

        //                    };

        //    if (items.Count() == 0)
        //    {
        //        return Json("404", Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(items, Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
  
        //}

        // GET: Admin/CandidateFile/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateFile candidateFile = await db.CandidateFiles.FindAsync(id);
            if (candidateFile == null)
            {
                return HttpNotFound();
            }
            return View(candidateFile);
        }

        // GET: Admin/CandidateFile/Create
        public ActionResult Create(int? candidateId)
        {
            //ViewBag.CandidateId = new SelectList(db.Candidates, "Id", "FirstName");

            if (candidateId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var items = from item in db.CandidateFiles.Include(c => c.Candidate)
                        where item.CandidateId == candidateId
                        select item;
            Candidate can = items.FirstOrDefault().Candidate;
 
            if (can == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new CandidateFile
            {
                CandidateId = candidateId ?? 0,
                FileName = can.LastName,
                //Description = can.LastName + ", " + can.FirstName
                Candidate = can,
                CandidateFileTypes =  db.CandidateFileTypes.ToList()
            };

            return View(model);
        }

        // POST: Admin/CandidateFile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CandidateId,FileName,Size,TimeStamp,FileData,CVFile,DateModified")] CandidateFile candidateFile, HttpPostedFileBase cvFile)
        {

            if (ModelState.IsValid)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    if (cvFile != null)
                    {
                        cvFile.InputStream.CopyTo(memoryStream);
                        candidateFile.FileData = memoryStream.ToArray();

                        var _filename = Path.GetFileName(cvFile.FileName);
                        CandidateFile tempCandidateFile = Helper.MakeWordDocThumbnail(memoryStream.ToArray(), _filename);
                        candidateFile.FileThumbnailData = tempCandidateFile.FileThumbnailData;
                        candidateFile.FileIconData = tempCandidateFile.FileIconData;
                        candidateFile.FileData = memoryStream.ToArray();
                        candidateFile.Author = tempCandidateFile.Author;
                        }
                }

                candidateFile.TimeStamp = DateTime.Now;
                candidateFile.FileName = Path.GetFileName(cvFile.FileName);
                candidateFile.Size = cvFile.ContentLength;

                Candidate can = db.Candidates.Find(candidateFile.CandidateId); //lazy loadin
                //candidateFile.Description = can.LastName + ", " + can.FirstName;


                db.CandidateFiles.Add(candidateFile);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateId = new SelectList(db.Candidates, "Id", "FirstName", candidateFile.CandidateId);
            return View(candidateFile);
        }

        // GET: Admin/CandidateFile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateFile candidateFile =  db.CandidateFiles.Find(id);
            if (candidateFile == null)
            {
                return HttpNotFound();
            }

            //candidateFile.Description = candidateFile.Candidate.LastName + ", " + candidateFile.Candidate.FirstName;


            Session["FileData"] = candidateFile.FileData;
            Session["FileIconData"] = candidateFile.FileIconData;
            Session["Author"] = candidateFile.Author;
            //Session["Description"] = candidateFile.Candidate.FullName;
            candidateFile.CandidateFileTypes =  db.CandidateFileTypes.ToList(); 

            //ViewBag.CandidateId = new SelectList(db.Candidates, "Id", "FirstName", candidateFile.CandidateId);
            return View(candidateFile);
        }

        // POST: Admin/CandidateFile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CandidateId,FileName,Size,TimeStamp,FileData,CVFile,Author,CandidateFileTypeId")] CandidateFile candidateFile, HttpPostedFileBase cvFile)
        {

            if (ModelState.IsValid)
            {

                if (cvFile != null)
                {
                    // pictureFile prop isn't mapped in the database...
                    candidateFile.CVFile = cvFile; // ... but I need to do this because PictureFile prop perform the necessary validations

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        cvFile.InputStream.CopyTo(memoryStream);
                        var _filename = Path.GetFileName(cvFile.FileName);

                        CandidateFile tempCandidateFile = Helper.MakeWordDocThumbnail(memoryStream.ToArray(), _filename);

                        candidateFile.FileThumbnailData = tempCandidateFile.FileThumbnailData;
                        candidateFile.FileIconData = tempCandidateFile.FileIconData;
                        candidateFile.FileName = _filename;

                        candidateFile.Size = tempCandidateFile.Size;
                        candidateFile.DateModified = tempCandidateFile.DateModified;

                        candidateFile.FileData = memoryStream.ToArray();
                        candidateFile.TimeStamp = tempCandidateFile.TimeStamp;
                        candidateFile.Author = tempCandidateFile.Author;
                         
                        Candidate can = db.Candidates.Find(candidateFile.CandidateId); //lazy loadin
                        //candidateFile.Description = candidateFile.Candidate.LastName + ", " + candidateFile.Candidate.FirstName;

                        Session["FileData"] = candidateFile.FileData;
                        Session["FileIconData"] = candidateFile.FileIconData;
                        Session["Author"] = candidateFile.Author;

                        tempCandidateFile = null;

                        db.Entry(candidateFile).State = EntityState.Modified; // default behaviour: all properties marked as modified
                    }
                } else
                {
                    //manipulating the entity state...
                    db.CandidateFiles.Attach(candidateFile);
                    db.Entry(candidateFile).State = EntityState.Modified; // all props modified, but...
                    db.Entry(candidateFile).Property(m => m.FileThumbnailData).IsModified = false; //there are some expeceptions!
                    db.Entry(candidateFile).Property(m => m.FileData).IsModified = false; //there are some expeceptions!
                    db.Entry(candidateFile).Property(m => m.FileIconData).IsModified = false; //there are some expeceptions!
                    db.Entry(candidateFile).Property(m => m.DateModified).IsModified = false; //there are some expeceptions!
                    db.Entry(candidateFile).Property(m => m.Author).IsModified = false; //there are some expeceptions!
                    //db.Entry(candidateFile).Property(m => m.Description).IsModified = false; //there are some expeceptions!

                }

                //db.Entry(candidateFile).State = EntityState.Modified;
                await db.SaveChangesAsync();

                if ((bool)Session["Filtered"] == true)
                {
                    return RedirectToAction("Index", new RouteValueDictionary(
                            new { controller = "CandidateFile", action = "Index", candidateId = candidateFile.CandidateId }));

                } else
                {
                    return RedirectToAction("Index");
                }

            }
            ViewBag.CandidateId = new SelectList(db.Candidates, "Id", "FirstName", candidateFile.CandidateId);
            candidateFile.FileData = (byte[])Session["FileData"];
            candidateFile.FileIconData = (byte[])Session["FileIconData"];
            candidateFile.Author = (string)Session["Author"];
            //candidateFile.Description = (string)Session["Description"];

            return View(candidateFile);
        }

        // GET: Admin/CandidateFile/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateFile candidateFile = await db.CandidateFiles.FindAsync(id);
            if (candidateFile == null)
            {
                return HttpNotFound();
            }
            return View(candidateFile);
        }

        // POST: Admin/CandidateFile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CandidateFile candidateFile = await db.CandidateFiles.FindAsync(id);
            db.CandidateFiles.Remove(candidateFile);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DownloadFile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateFile candidateFile = await db.CandidateFiles.FindAsync(id);
            if (candidateFile == null)
            {
                return HttpNotFound();
            }

            try
            {
                // Invalid extensions will throw an exception (cos they aren't included in the ContentType Dict
                string _extension = Path.GetExtension(candidateFile.FileName);
                string _mimeType = Helper.ContentType[_extension];
                
                return File(candidateFile.FileData, _mimeType, candidateFile.FileName);
            } catch
            {
                return new HttpStatusCodeResult(500);
            }


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
