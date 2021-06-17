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

namespace Walnut.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Article
        public async Task<ActionResult> Index()
        {
            ICollection<Article> articles = await db.Articles.Include(a => a.ArticleTypes).ToListAsync();
            
            //foreach (Article article in articles)
            //{
            //    article.ArticleTypes.Add(db.ArticleTypes.Find(article.ArticleTypeId));                
            //}

            return View(articles);
        }

        // GET: Admin/Article/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Admin/Article/Create
        public ActionResult Create()
        {
            var model = new Article
            {


                ArticleTypes = db.ArticleTypes.ToList()


            };
            return View(model);

        }

        // POST: Admin/Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Subtitle,Body,AuthorId,ArticleTypeId,IntroParagraph,SourceLink,DatePublished,ArticleImage")] Article article, HttpPostedFileBase pictureFile)
        {

            article.TimeStamp = DateTime.Now;

            if (pictureFile != null)
            {
                article.PictureFile = pictureFile; // ... but I need to do this because PictureFile prop perform the necessary validations

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pictureFile.InputStream.CopyTo(memoryStream);
                    //candidate.ImageData = memoryStream.ToArray();
                    article.ArticleImage = Helper.MakeThumbnail(memoryStream.ToArray(), 0.40F);
                    Session["ArticleImage"] = article.ArticleImage;
                }

               // db.Entry(article).State = EntityState.Modified;
            }
            else // no picture!!
            {
                //manipulating the entity state...
                //db.Articles.Attach(article);
                //db.Entry(article).State = EntityState.Modified; // all props modified, but...
                //db.Entry(article).Property(m => m.ArticleImage).IsModified = false; //there are some expeceptions!                                                                   //db.Entry(candidateFile).Property(m => m.Description).IsModified = false; //there are some expeceptions!
            }

            if (ModelState.IsValid)
            {
                article.ArticleImage = (byte[])Session["ArticleImage"];
                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //article.ArticleImage = (byte[])Session["ArticleImage"];
            article.ArticleTypes = await db.ArticleTypes.ToListAsync();
            article.ArticleImage = (byte[])Session["ArticleImage"];

            return View(article);
        }

        // GET: Admin/Article/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
 
            if (article == null)
            {
                return HttpNotFound();
            }

            article.ArticleTypes = await db.ArticleTypes.ToListAsync();
            Session["ArticleImage"] = article.ArticleImage;
            return View(article);
        }

        // POST: Admin/Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Subtitle,Body,AuthorId,ArticleTypeId,IntroParagraph,SourceLink,DatePublished,ArticleImage,PictureFile")] Article article, HttpPostedFileBase pictureFile)
        {
            article.TimeStamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (pictureFile != null)
                {
                    article.PictureFile = pictureFile; // ... but I need to do this because PictureFile prop perform the necessary validations

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        pictureFile.InputStream.CopyTo(memoryStream);
                        //article.ArticleImage = memoryStream.ToArray();
                        article.ArticleImage = Helper.MakeThumbnail(memoryStream.ToArray(), 0.40F);
                        Session["ArticleImage"] = article.ArticleImage;
                    }

                    db.Entry(article).State = EntityState.Modified; // default behaviour: all properties marked as modified

                } else
                {
                    //manipulating the entity state...
                    db.Articles.Attach(article);
                    db.Entry(article).State = EntityState.Modified; // all props modified, but...
                    db.Entry(article).Property(m => m.ArticleImage).IsModified = false; //there are some expeceptions!
                     //db.Entry(candidateFile).Property(m => m.Description).IsModified = false; //there are some expeceptions!
                }

                //db.Entry(candidateFile).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //db.Entry(article).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            article.ArticleImage = (byte[])Session["ArticleImage"];
            article.ArticleTypes = await db.ArticleTypes.ToListAsync();
            return View(article);
        }

        // GET: Admin/Article/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Admin/Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            db.Articles.Remove(article);
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
