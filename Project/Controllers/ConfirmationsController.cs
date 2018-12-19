using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project;
using Project.Models;

namespace Project.Controllers
{
    public class ConfirmationsController : Controller
    {
        private ProjectData db = new ProjectData();

        // GET: Confirmations
        public ActionResult Index()
        {
            if (TempData["auth"] != null)
                ViewBag.auth = TempData["auth"];
            else
                ViewBag.auth = "";
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            var confirmations = db.Confirmations.Include(c => c.employee);
            return View(confirmations.ToList());
        }

        // GET: Confirmations/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirmation confirmation = db.Confirmations.Find(id);
            if (confirmation == null)
            {
                return HttpNotFound();
            }
            return View(confirmation);
        }
        
        // GET: Confirmations/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            List<string> li = new List<string>
            {
                "Training",
                "Educational_Details",
                "Workshop"
            };
            ViewBag.li = li;
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name");
            return View();
        }

        // POST: Confirmations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConfirmationId,EmployeeId,ftype")] Confirmation confirmation,HttpPostedFileBase file)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(confirmation.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Confirmations");
            }
            var f = 0;
            if(confirmation.ftype.Equals("Educational") || confirmation.ftype.Equals("Training") || confirmation.ftype.Equals("Workshop"))
            {
                f = 1;
            }
            else
            {
                ViewBag.error = "FileType must be Eductional,Training or Workshop";
            }
            try
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    var em = db.employees.Find(confirmation.EmployeeId);
                    string path = @"C:\Users\Kishan\source\repos\Project\Project\confirmation_certificate\"+ em.Name;
                    if (!(Directory.Exists(path)))
                    {
                        Directory.CreateDirectory(path);
                        string path1;
                        path1 = path + @"\" + "Educational";
                        Directory.CreateDirectory(path1);
                        path1 = path + @"\" + "Workshop";
                        Directory.CreateDirectory(path1);
                        path1 = path + @"\" + "Training";
                        Directory.CreateDirectory(path1);
                    }
                    path = path + @"\" + confirmation.ftype + @"\" + filename;
                    file.SaveAs(path);
                    confirmation.fname = filename;
                    confirmation.status = "Not Verified";
                    f = 1;
                }
                ViewBag.Message = "File Uploaded Successfully";
            }
            catch
            {
                f = 0;
                ViewBag.Message = "File Upload Failed";
            }
            if (f==1)
            {
                db.Confirmations.Add(confirmation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", confirmation.EmployeeId);
            return View(confirmation);
        }
        public ActionResult verify()
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Confirmations");
            }
            var confirmations = db.Confirmations.Where(c => c.status.Equals("Not Verified"));
            return View(confirmations.ToList());
        }
        public ActionResult verify1(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Confirmations");
            }
            Confirmation con = db.Confirmations.Find(id);
            string path = @"C:\Users\Kishan\source\repos\Project\Project\confirmation_certificate\" + con.employee.Name + @"\" + con.ftype +@"\" + con.fname;
            string path1 = @"C:\Users\Kishan\source\repos\Project\Project\directory\" + con.employee.Name + @"\";
            if (con.ftype == "Educational")
            {
                path1 = path1 + @"Educational_Certificates\" + con.fname;
            }
            else if(con.ftype.Equals("Workshop"))
            {
                path1 = path1 + @"Workshop_Certificates\" + con.fname;
            }
            else if(con.ftype.Equals("Training"))
            {
                path1 = path1 + @"Training_Certificates\" + con.fname;
            }
            System.IO.File.Move(path,path1);
            con.status = "Verified";
            db.Entry(con).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("verify","Confirmations");
        }
        public ActionResult reject1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Confirmations");
            }
            Confirmation con = db.Confirmations.Find(id);
            string path = @"C:\Users\Kishan\source\repos\Project\Project\confirmation_certificate\" + con.employee.Name + @"\" + con.ftype;
            //System.IO.File.Move(path, path1);
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo f in di.GetFiles())
            {
                if (f.Name.Equals(con.fname))
                {
                    f.Delete();
                }
            }
            con.status = "Rejected";
            db.Entry(con).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("verify", "Confirmations");
        }
        // GET: Confirmations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            var c = db.Confirmations.Find(id).EmployeeId;
            Confirmation confirmation;
            if (!Session["id"].Equals(c) && !Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Confirmations");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            confirmation = db.Confirmations.Find(id);

            if (confirmation == null)
            {
                return HttpNotFound();
            }
            return View(confirmation);
        }

        // POST: Confirmations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            var c = db.Confirmations.Find(id).EmployeeId;
            Confirmation confirmation;
            if (!Session["id"].Equals(c) && !Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Confirmations");
            }
            confirmation = db.Confirmations.Find(id);
            string fn=confirmation.fname;
            string ft = confirmation.ftype;
            string path;
            var em = db.employees.Find(confirmation.EmployeeId);
            if (confirmation.status.Equals("Not Verified"))
            {
                path = @"C:\Users\Kishan\source\repos\Project\Project\confirmation_certificate\" + em.Name + @"\" + ft;
            }
            else if((confirmation.status.Equals("Verified") || confirmation.status.Equals("Rejected")) && Session["Role"].ToString().ToLower().Equals("admin"))
            {
                path = @"C:\Users\Kishan\source\repos\Project\Project\directory\" + em.Name + @"\";
                if (ft.Equals("Educational"))
                    path += "Educational_Certificates";
                else if (ft.Equals("Training"))
                    path += "Training_Certificates";
                else if(ft.Equals("Workshop"))
                    path += "Workshop_Certificates";
            }
            else
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Confirmations");
            }
            if (!confirmation.Equals("Rejected"))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (FileInfo f in di.GetFiles())
                {
                    if (f.Name.Equals(fn))
                    {
                        f.Delete();
                    }
                }
            }
            db.Confirmations.Remove(confirmation);
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
