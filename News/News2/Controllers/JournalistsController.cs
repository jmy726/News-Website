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
using System.IO;

namespace News2.Controllers
{
    public class JournalistsController : Controller
    {
        private NewsEntities db = new NewsEntities();

        // GET: Journalists
        public ActionResult Index()
        {
            return View(db.Journalists.OrderBy(j => j.jourDob).ToList());
        }

        public ActionResult UserIndex()
        {
            var journalists = db.Journalists.Include(j => j.ProfileImgs);
            //foreach ( var aJournalist in journalists)
            //{
            //    var image = db.ProfileImgs.FirstOrDefault((i => i.jourId == aJournalist.JourID));
            //    if (image != null)
            //    {
            //        ViewBag.img = image.img;
            //    }
            //    else
            ViewBag.img = db.ProfileImgs.Find(13).img;
            //}
            return View(journalists.OrderBy(j => j.jourDob).ToList());
        }

        // GET: Journalists/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journalist journalist = db.Journalists.Find(id);
            if (journalist == null)
            {
                return HttpNotFound();
            }
            ProfileImg imgRecord = journalist.ProfileImgs.FirstOrDefault(i => i.jourId == id);
            if (imgRecord != null)
            {
                ViewBag.img = imgRecord.img;
                ViewBag.message = "";
            }
            else
            {
                ViewBag.img = db.ProfileImgs.Find(13).img;
                ViewBag.message = "There are no profile image uploaded.";
            }
            return View(journalist);
        }

        // GET: Journalists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Journalists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JourID,officeName,jourFName,jourLName,jourDob,phone,email,officeStreet,officeSuburb,officeState")] Journalist journalist)
        {
            if (ModelState.IsValid)
            {
                db.Journalists.Add(journalist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(journalist);
        }

        // GET: Journalists/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journalist journalist = db.Journalists.Find(id);
            if (journalist == null)
            {
                return HttpNotFound();
            }
            return View(journalist);
        }

        // POST: Journalists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JourID,officeName,jourFName,jourLName,jourDob,phone,email,officeStreet,officeSuburb,officeState")] Journalist journalist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journalist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(journalist);
        }

        // GET: Journalists/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journalist journalist = db.Journalists.Find(id);
            if (journalist == null)
            {
                return HttpNotFound();
            }
            return View(journalist);
        }

        // POST: Journalists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Journalist journalist = db.Journalists.Find(id);
            db.Journalists.Remove(journalist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AddImg()
        {
            ProfileImg proImg = new ProfileImg();
            ViewBag.fileName = "noImg.png";
            return View(proImg);
        }
        [HttpPost]
        public ActionResult AddImg(ProfileImg model, HttpPostedFileBase imageUpload)
        {
            var db = new NewsEntities();
            string currentUserId = User.Identity.GetUserId();
            model.jourId = currentUserId;
            ProfileImg img1 = db.ProfileImgs.FirstOrDefault(i=>i.jourId == currentUserId);
            //if (db.ProfileImgs.Where(m => model.jourId == currentUserId) == null)
            //{
            if (img1 == null)
            {
              if (imageUpload != null)
                {
                    model.img = new byte[imageUpload.ContentLength];
                    imageUpload.InputStream.Read(model.img, 0, imageUpload.ContentLength);
                    var fileName = Path.GetFileName(imageUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    imageUpload.SaveAs(path);
                    ViewBag.fileName = imageUpload.FileName;
                }
                db.ProfileImgs.Add(model);
                db.SaveChanges();
                ViewBag.Message = "File uploaded successfully.";
            }
            else
                ViewBag.Message = "You have already upload an profile image before.";
            return View(model);
        }

        public string GetProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                var db = new NewsEntities();
                var journalist = db.Journalists.Find(userId);
                if (journalist != null)
                {
                    if (journalist.ProfileImgs.Count() != 0)
                    {
                        byte[] img = journalist.ProfileImgs.FirstOrDefault(p => p.jourId == journalist.JourID).img;
                        return @System.Convert.ToBase64String(img);
                    }
                    else
                    {
                        byte[] noImg = db.ProfileImgs.Find(13).img;
                        return @System.Convert.ToBase64String(noImg);
                    }
                }
                else
                {
                    byte[] noImg = db.ProfileImgs.Find(13).img;
                    return @System.Convert.ToBase64String(noImg);
                }
            }
            else
            {
                byte[] noImg = db.ProfileImgs.Find(13).img;
                return @System.Convert.ToBase64String(noImg);
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
