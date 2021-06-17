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
    public class ArticleTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ArticleType
        public async Task<ActionResult> Index()
        {
            return View(await db.ArticleTypes.ToListAsync());
        }

        // GET: Admin/ArticleType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleType articleType = await db.ArticleTypes.FindAsync(id);
            if (articleType == null)
            {
                return HttpNotFound();
            }
            return View(articleType);
        }

        // GET: Admin/ArticleType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ArticleType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,HTMLTemplate")] ArticleType articleType)
        {
            if (ModelState.IsValid)
            {
                db.ArticleTypes.Add(articleType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(articleType);
        }

        // GET: Admin/ArticleType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleType articleType = await db.ArticleTypes.FindAsync(id);
            if (articleType == null)
            {
                return HttpNotFound();
            }
            return View(articleType);
        }

        // POST: Admin/ArticleType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,HTMLTemplate")] ArticleType articleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articleType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(articleType);
        }

        // GET: Admin/ArticleType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleType articleType = await db.ArticleTypes.FindAsync(id);
            if (articleType == null)
            {
                return HttpNotFound();
            }
            return View(articleType);
        }

        // POST: Admin/ArticleType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ArticleType articleType = await db.ArticleTypes.FindAsync(id);
            db.ArticleTypes.Remove(articleType);
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
