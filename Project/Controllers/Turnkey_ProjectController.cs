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
    public class Turnkey_ProjectController : Controller
    {
        private ProjectData db = new ProjectData();

        // GET: Turnkey_Project
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
            var turnkey_Project = db.Turnkey_Project.Include(t => t.employee);
            return View(turnkey_Project.ToList());
        }

        // GET: Turnkey_Project/Details/5
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
            Turnkey_Project turnkey_Project = db.Turnkey_Project.Find(id);
            if (turnkey_Project == null)
            {
                return HttpNotFound();
            }
            return View(turnkey_Project);
        }

        // GET: Turnkey_Project/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name");
            return View();
        }

        // POST: Turnkey_Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Turnkey_ProjectId,EmployeeId,Project_Name,Begin_Date,End_Date,Role,Description,Team_Size")] Turnkey_Project turnkey_Project)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(turnkey_Project.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Turnkey_Project");
            }
            if (ModelState.IsValid)
            {
                db.Turnkey_Project.Add(turnkey_Project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", turnkey_Project.EmployeeId);
            return View(turnkey_Project);
        }

        // GET: Turnkey_Project/Edit/5
        public ActionResult Edit(int? id)
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
            Turnkey_Project turnkey_Project = db.Turnkey_Project.Find(id);
            if (turnkey_Project == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", turnkey_Project.EmployeeId);
            return View(turnkey_Project);
        }

        // POST: Turnkey_Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Turnkey_ProjectId,EmployeeId,Project_Name,Begin_Date,End_Date,Role,Description,Team_Size")] Turnkey_Project turnkey_Project)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(turnkey_Project.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Turnkey_Project");
            }
            if (ModelState.IsValid)
            {
                db.Entry(turnkey_Project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", turnkey_Project.EmployeeId);
            return View(turnkey_Project);
        }

        // GET: Turnkey_Project/Delete/5
        public ActionResult Delete(int? id)
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
            Turnkey_Project turnkey_Project = db.Turnkey_Project.Find(id);
            if (turnkey_Project == null)
            {
                return HttpNotFound();
            }
            return View(turnkey_Project);
        }

        // POST: Turnkey_Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            var c = db.Turnkey_Project.Find(id).EmployeeId;
            if (!Session["id"].Equals(c))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Turnkey_Project");
            }
            Turnkey_Project turnkey_Project = db.Turnkey_Project.Find(id);
            db.Turnkey_Project.Remove(turnkey_Project);
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
