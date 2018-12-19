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
using System.IO;

namespace Project.Controllers
{
    public class Educational_detailsController : Controller
    {
        private ProjectData db = new ProjectData();

        // GET: Educational_details
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
            var educational_details = db.Educational_details.Include(e => e.employee);
            return View(educational_details.ToList());
        }

        // GET: Educational_details/Details/5
        public ActionResult Details(int? id)
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
            Educational_details educational_details = db.Educational_details.Find(id);
            if (educational_details == null)
            {
                return HttpNotFound();
            }
            return View(educational_details);
        }

        // GET: Educational_details/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name");
            return View();
        }

        // POST: Educational_details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Educational_detailsId,EmployeeId,Start_Date,End_Date,Course_Name,Description")] Educational_details educational_details)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(educational_details.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Educational_details");
            }
            if (ModelState.IsValid)
            {
                db.Educational_details.Add(educational_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", educational_details.EmployeeId);
            return View(educational_details);
        }

        // GET: Educational_details/Edit/5
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
            Educational_details educational_details = db.Educational_details.Find(id);
            if (educational_details == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", educational_details.EmployeeId);
            return View(educational_details);
        }

        // POST: Educational_details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Educational_detailsId,EmployeeId,Start_Date,End_Date,Course_Name,Description")] Educational_details educational_details)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(educational_details.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Educational_details");
            }
            if (ModelState.IsValid)
            {
                db.Entry(educational_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", educational_details.EmployeeId);
            return View(educational_details);
        }

        // GET: Educational_details/Delete/5
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
            Educational_details educational_details = db.Educational_details.Find(id);
            if (educational_details == null)
            {
                return HttpNotFound();
            }
            return View(educational_details);
        }

        // POST: Educational_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            var c = db.Educational_details.Find(id).EmployeeId;
            if (!Session["id"].Equals(c))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Educational_details");
            }
            Educational_details educational_details = db.Educational_details.Find(id);
            db.Educational_details.Remove(educational_details);
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
