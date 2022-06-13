using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTest.Models;

namespace FortuneTest.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Forum.AsNoTracking().Where(m => m.Author == User.Identity.Name).ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forum.Find(id);
            if (forum == null || forum.Author != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string title, string text, Forum forum)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                var data = new Forum
                {
                    Author = User.Identity.Name,
                    Title = title,
                    Text = text,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                db.Forum.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return HttpNotFound();
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forum.Find(id);
            if (forum == null || forum.Author != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string title, string text, Forum forum)
        {
            if (ModelState.IsValid)
            {
                var data = new Forum
                {
                    Id = id,
                    Author = User.Identity.Name,
                    Title = title,
                    Text = text,
                    UpdateDate = DateTime.Now
                };

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forum);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forum.Find(id);
            if (forum == null || forum.Author != User.Identity.Name)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Forum.Find(id);
            db.Forum.Remove(forum);
            db.SaveChanges();
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
