using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using News2.Models;
using Microsoft.AspNet.Identity;

namespace News2.Controllers
{
    public class ArticlesController : Controller
    {
        private NewsEntities db = new NewsEntities();

        // GET: Articles
        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Journalist);
            return View(articles.OrderBy(a => a.datePub).ToList());
        }

        [Authorize]
        public ActionResult IndexUserNames()
        {
            //return View(db.Artiles.ToList());
            string currentUserId = User.Identity.GetUserId();
            return View(db.Articles.Where(m => m.jourID == currentUserId).ToList());
        }


        public ActionResult UserIndex()
        {
            var articles = db.Articles.Include(a => a.Journalist);
            return View(articles.OrderBy(a => a.datePub).ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult UserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            //ViewBag.jourID = new SelectList(db.Journalists, "JourID", "officeName");
            //return View();
            ViewBag.jourID = db.Journalists.OrderBy(j => j.jourFName).Select(j => new SelectListItem()
            {
                Value = j.JourID.ToString(),
                Text = j.jourFName + " " + j.jourLName
            });
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ISSN,jourID,headline,datePub,text,topic,comments,page")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.jourID = db.Journalists.Select(j => new SelectListItem()
            {
                Value = j.JourID.ToString(),
                Text = j.jourFName + " " + j.jourLName
            });
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.jourID = db.Journalists.OrderBy(j => j.jourFName).Select(j => new SelectListItem()
            {
                Value = j.JourID.ToString(),
                Text = j.jourFName + " " + j.jourLName
            });
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ISSN,jourID,headline,datePub,text,topic,comments,page")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jourID = db.Journalists.OrderBy(j => j.jourFName).Select(j => new SelectListItem()
            {
                Value = j.JourID.ToString(),
                Text = j.jourFName + " " + j.jourLName
            });
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: Artiles/Create
        public ActionResult CreateIndividual()
        {
            Article article = new Article();
            string currentUserId = User.Identity.GetUserId();
            article.jourID = currentUserId;
            return View(article);
        }
        // POST: Artiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize]
        public ActionResult CreateIndividual([Bind(Include = "ISSN,jourID,headline,datePub,text,topic,comments,page")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("IndexUserNames");
            }

            return View(article);
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
