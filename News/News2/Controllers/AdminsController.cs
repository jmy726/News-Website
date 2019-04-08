using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using News2.Models;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace News2.Controllers
{
    public class AdminsController : Controller
    {
        private NewsEntities db = new NewsEntities();

        [Authorize(Roles = "Admin")]
        public ActionResult AdminRedirect()
        {
            return View();
        }

        public ActionResult SendEmail()
        {
            ViewBag.Message = "Sending Email.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                var addresses = model.ToEmail;
                foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(new MailAddress(address));
                }
                /*message.To.Add(new MailAddress(model.ToEmail)); */ 
                message.From = new MailAddress("shua0012@student.monash.edu");  // replace with valid value
                message.Subject = "My MVC Email";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                if (model.Upload != null && model.Upload.ContentLength > 0)
                {
                    message.Attachments.Add(new Attachment(model.Upload.InputStream, Path.GetFileName(model.Upload.FileName)));
                }


                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "shua0012@student.monash.edu",  // replace with valid value
                        //Password = ""  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.monash.edu.au";
                    //smtp.Port = 587;
                    //smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    //return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adminID,email,phone")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminID,email,phone")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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

        public ActionResult SendMultipleEmail()
        {
            var journalists = db.Journalists.ToList();
            //var paper = db.Papers.Find(paperId);
            CheckBoxList chklist = new CheckBoxList
            {
                checklist = new List<CheckBox>(),
            };
            foreach (var journalist in journalists)//put all the journalists into the checkboxlist
            {
                var checkbox1 = new CheckBox
                {
                    IsSelected = false,
                    JournalistId = journalist.JourID,
                    JournalistFirstName = journalist.jourFName,
                    JournalistLastName = journalist.jourLName
                };
                Journalist aJournalist = db.Journalists.Find(journalist.JourID);
                if (aJournalist != null)
                {
                    checkbox1.Email = journalist.email;
                }
                //AspNetUser aspUser = db.AspNetUsers.Find(journalist.JourID);
                //if (aspUser != null)
                //{
                //    checkbox1.Email = aspUser.Email;
                //}
                chklist.checklist.Add(checkbox1);
            }
            //var tuple = new Tuple<CheckBoxList, EmailFormModel>(chklist,new EmailFormModel());
            return View(chklist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMultipleEmail(CheckBoxList model)
        {
            if (model.checklist.Count(x => x.IsSelected) > 0)
            {
                var selectedEmail = model.GetSelectedEmail();
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                foreach (var journalistEmail in selectedEmail)
                {
                    //string journalistEmail = db.Journalists.Find(journalistId).email;
                    message.To.Add(new MailAddress(journalistEmail));
                }
                message.From = new MailAddress("shua0012@student.monash.edu");  // replace with valid value
                message.Subject = "MVC Email to Journalist";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.message);
                message.IsBodyHtml = true;
                //if (model.Upload != null && model.Upload.ContentLength > 0)
                //{
                //    message.Attachments.Add(new Attachment(model.Upload.InputStream, Path.GetFileName(model.Upload.FileName)));
                //}

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "shua0012@student.monash.edu",  // replace with valid value
                                                                   //Password = ""  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.monash.edu.au";
                    //smtp.Port = 587;
                    //smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
                //}
                //}

            }
            else
            {
                ViewBag.Message = "Please select at least 1 email";
            }
                return View(model);
        }

        public List<Journalist> GetJournalists()
        {
            List<Journalist> journalists = db.Journalists.ToList();
            return journalists;
        }

        public List<Admin> GetAdmins()
        {
            List<Admin> admins = db.Admins.ToList();
            return admins;
        }

        public ActionResult SearchUser()
        {
            UserViewModel mymodel = new UserViewModel();
            mymodel.Journalists = GetJournalists();
            mymodel.Admins = GetAdmins();
            return View(mymodel);
        }
    }
}
