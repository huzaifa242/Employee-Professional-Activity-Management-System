using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project;
using Project.Models;

namespace Project.Controllers
{
    public class AuthsController : Controller
    {
        private ProjectData db = new ProjectData();

        // GET: Auths
        public ActionResult Index()
        {
            if(Session["id"]==null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if(!Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            var auths = db.auths.Include(a => a.employee);
            return View(auths.ToList());
        }

        // GET: Auths/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auth auth = db.auths.Find(id);
            if (auth == null)
            {
                return HttpNotFound();
            }
            return View(auth);
        }

        // GET: Auths/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            ViewBag.AuthId = new SelectList(db.employees, "EmployeeId", "Name");
            return View();
        }

        // POST: Auths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthId,password,Auth_Role")] Auth auth)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            var f = 0;
            ViewBag.error = "";
            if (!auth.Auth_Role.ToLower().Equals("admin") && !auth.Auth_Role.ToLower().Equals("user"))
            {
                f = 1;
                ViewBag.error += "  Invalid Role.";
            }
            if(auth.password.Length>=10)
            {
                f = 1;
                ViewBag.error += "   password length must be less than 10";
            }
            if (ModelState.IsValid && f==0)
            {
                db.auths.Add(auth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthId = new SelectList(db.employees, "EmployeeId", "Name", auth.AuthId);
            return View(auth);
        }

        // GET: Auths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auth auth = db.auths.Find(id);
            if (auth == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthId = new SelectList(db.employees, "EmployeeId", "Name", auth.AuthId);
            return View(auth);
        }

        // POST: Auths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthId,password,Auth_Role")] Auth auth)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            var f = 0;
            ViewBag.error = "";
            if (!auth.Auth_Role.ToLower().Equals("admin") && !auth.Auth_Role.ToLower().Equals("user"))
            {
                f = 1;
                ViewBag.error += "  Invalid Role.";
            }
            if (auth.password.Length >= 10)
            {
                f = 1;
                ViewBag.error += "   password length must be less than 10";
            }
            if (ModelState.IsValid && f==0)
            {
                db.Entry(auth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthId = new SelectList(db.employees, "EmployeeId", "Name", auth.AuthId);
            return View(auth);
        }

        // GET: Auths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auth auth = db.auths.Find(id);
            if (auth == null)
            {
                return HttpNotFound();
            }
            return View(auth);
        }

        // POST: Auths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["Role"].ToString().ToLower().Equals("admin"))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            Auth auth = db.auths.Find(id);
            db.auths.Remove(auth);
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
