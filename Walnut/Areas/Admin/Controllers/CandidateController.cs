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
using X.PagedList;
using X.PagedList.Mvc;
using System.IO;
using System.Drawing;

namespace Walnut.Areas.Admin.Controllers
{
    public class CandidateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Candidate
        public ActionResult Index(int? page)
        {
            // keep the next two lines of code for future reference; we need these vars to 
            // create the pages url's in the _pagedListPartial partial view.
            //ViewBag.Controller = ControllerContext.RouteData.Values["controller"];
            //ViewBag.Action = ControllerContext.RouteData.Values["action"];

            //following fields must be set to make pagination html helper work!
            ViewBag.CurrentPage = page ?? 1;
            ViewBag.RecordsPerPage = 10;
            ViewBag.TotalRecords = db.Candidates.ToList().Count;

            return View( db.Candidates.ToList().ToPagedList(page ?? 1, (int)ViewBag.RecordsPerPage));

        }


        // GET: Admin/Candidate/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = await db.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: Admin/Candidate/Create
        public async Task<ActionResult> Create()
        {
            var model = new Candidate
            {
                DOB = DateTime.Today,
                Genders = await db.Genders.ToListAsync(),
                WorkTypes = await db.WorkTypes.ToListAsync(),
                Countries = Helper.GetCountryLIst()
               
            };

            return View(model);
        }

        // POST: Admin/Candidate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,AddressLine1,AddressLine2,AddressLine3,Postcode,Phone,Mobile,Email,GenderId,DOB,CountryId,VisaNeeded,VisaInfo,DriversLicense,WorkTypeId,Genders,WorkTypes,PictureFile")] Candidate candidate, HttpPostedFileBase pictureFile)
        {
            if (ModelState.IsValid)
            {

                if (pictureFile != null)
                {
                    // keep next line of code for future reference (just in case I need to create a file from the memory stream
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmm.") + pictureFile.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();

                    // pictureFile prop isn't mapped in the database...
                    candidate.PictureFile = pictureFile; // ... but I need to do this because PictureFile prop perform the necessary validations
                    //candidate.ImageData = null;

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        pictureFile.InputStream.CopyTo(memoryStream);
                        //candidate.ImageData = memoryStream.ToArray();
                        candidate.ImageData = Helper.MakeThumbnail(memoryStream.ToArray(), 0.40F);
                    }
                }

                db.Candidates.Add(candidate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            candidate.Genders = await db.Genders.ToListAsync();
            candidate.WorkTypes = await db.WorkTypes.ToListAsync();
            candidate.Countries = Helper.GetCountryLIst();
            return View(candidate);
        }

        [HttpGet]
        public ActionResult CandidateFiles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //db.Configuration.ProxyCreationEnabled = false; // to remove loading child entities (otherwise we get an exception)

            List<CandidateFile> lista = db.CandidateFiles.ToList(); //original list
            IEnumerable<CandidateFile> items = from item in lista   //new list
                                               where item.CandidateId == id
                                               select new CandidateFile // linq queries are IEnumerable<T>
                                               {
                                                   Id = item.Id,
                                                   //Description = item.Description,
                                                   FileName = item.FileName,
                                                   Size = item.Size

                                               };

            return View("~/Views/Shared/_CandidateFilesPartial.cshtml", items);
            //return Json(items, Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Candidate/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = await db.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }


            // for testing purpoueses
            string NombreFichero;
            try
            {
                NombreFichero = candidate.CandidateFiles.ToList().FirstOrDefault().FileName;
            } catch
            {
                NombreFichero = "";
            }

            CandidateFile nuevoFichero = new CandidateFile();

            candidate.Genders = await db.Genders.ToListAsync();
            candidate.WorkTypes = await db.WorkTypes.ToListAsync();
            candidate.Countries = Helper.GetCountryLIst();

            Session["ImageData"] = candidate.ImageData;

            return View(candidate);
        }

        // POST: Admin/Candidate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,AddressLine1,AddressLine2,AddressLine3,Postcode,Phone,Mobile,Email,GenderId,DOB,CountryId,VisaNeeded,VisaInfo,DriversLicense,WorkTypeId,PictureFile,ImageData")] Candidate candidate, HttpPostedFileBase pictureFile)
        {
  
            if (ModelState.IsValid)
            {
                if (pictureFile != null) //We have a picture from http post action?
                {
                    // keep next line of code for future reference (just in case I need to create a file from the memory stream
                    // var fileName = DateTime.Now.ToString("yyyyMMddHHmm.") + pictureFile.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();

                    // pictureFile prop isn't mapped in the database...
                   candidate.PictureFile = pictureFile; // ... but I need to do this because PictureFile prop perform the necessary validations

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        pictureFile.InputStream.CopyTo(memoryStream);
                        //candidate.ImageData = memoryStream.ToArray(); //now we can generate thumbnails

                        candidate.ImageData = Helper.MakeThumbnail(memoryStream.ToArray(), 0.40F);

                        Session["ImageData"] = candidate.ImageData;
                    }

                    db.Entry(candidate).State = EntityState.Modified; // default behaviour: all properties marked as modified
                } else //we don't have a picture from http post action!
                {

                    db.Candidates.Attach(candidate);
                    db.Entry(candidate).State = EntityState.Modified; // all props modified, but...
                    db.Entry(candidate).Property(m => m.ImageData).IsModified = false; //there are some expeceptions!
                }

                await db.SaveChangesAsync(); // save changes allways
                return RedirectToAction("Index");
            }

            candidate.CandidateFiles = await db.CandidateFiles.Where(c => c.CandidateId == candidate.Id).ToListAsync();
            candidate.Genders = await db.Genders.ToListAsync();
            candidate.WorkTypes = await db.WorkTypes.ToListAsync();
            candidate.Countries = Helper.GetCountryLIst();

            candidate.ImageData = (byte[])Session["ImageData"];

            return View(candidate);
        }

        // GET: Admin/Candidate/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = await db.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Admin/Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Candidate candidate = await db.Candidates.FindAsync(id);
            db.Candidates.Remove(candidate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //GET: Admin/CandidateFile/Delete/5
        // Delete the CHILD ENTITY "CandidateFile"
        public async Task<ActionResult> DeleteFile(int? id)
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

            return PartialView("~/Views/Shared/_DeleteFilePartial.cshtml", candidateFile);

        }

        // POST: Admin/CandidateFile/Delete/5
        // Delete the CHILD ENTITY "CandidateFile"
        [HttpPost, ActionName("DeleteFile")]
        //[ValidateAntiForgeryToken] // be carefull with this and ajax action link
        public ActionResult DeleteFileConfirmed(int id)
        {
            CandidateFile candidateFile =  db.CandidateFiles.Find(id);
            db.CandidateFiles.Remove(candidateFile);
            db.SaveChanges();

            // Make again the query to retrieve ONLY the CURRENT CANDIDATE FILES:
            List<CandidateFile> lista = db.CandidateFiles.ToList(); //entire list
            IEnumerable<CandidateFile> items = from item in lista   //linq query to get a new filtered list
                                               where item.CandidateId == candidateFile.CandidateId
                                               select new CandidateFile // linq queries are IEnumerable<T>
                                               {
                                                   Id = item.Id,
                                                   //Description = item.Description,
                                                   FileName = item.FileName,
                                                   Size = item.Size
                                               };

            return PartialView("~/Views/Shared/_CandidateFilesPartial.cshtml", items);
        }


        [HttpGet]
        public ActionResult ToJSON(int? candidateId)
        {
            if ( candidateId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Configuration.ProxyCreationEnabled = false; // to remove loading child entities (otherwise we get an exception)

            List<Candidate> candidates = db.Candidates.ToList();
            IEnumerable<Candidate> items = from candidate in candidates
                                           where candidate.Id == candidateId
                                           select new Candidate
                                               {
                                                   Id = candidate.Id,
                                                   LastName = candidate.LastName,
                                                   FirstName = candidate.FirstName
                                               };

            if (items.Count() == 0)
            {
                return Json("404", Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);
            }

            return Json(items, Helper.ContentType[".json"], JsonRequestBehavior.AllowGet);

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
