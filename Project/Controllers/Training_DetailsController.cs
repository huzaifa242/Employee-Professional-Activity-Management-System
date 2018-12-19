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
    public class Training_DetailsController : Controller
    {
        private ProjectData db = new ProjectData();

        // GET: Training_Details
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
            var training_Details = db.Training_Details.Include(t => t.employee);
            return View(training_Details.ToList());
        }

        // GET: Training_Details/Details/5
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
            Training_Details training_Details = db.Training_Details.Find(id);
            if (training_Details == null)
            {
                return HttpNotFound();
            }
            return View(training_Details);
        }

        // GET: Training_Details/Create
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

        // POST: Training_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Training_DetailsId,EmployeeId,Begin_Date,End_Date,Subject,Description")] Training_Details training_Details)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(training_Details.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Training_Details");
            }
            if (ModelState.IsValid)
            {
                db.Training_Details.Add(training_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", training_Details.EmployeeId);
            return View(training_Details);
        }

        // GET: Training_Details/Edit/5
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
            Training_Details training_Details = db.Training_Details.Find(id);
            if (training_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", training_Details.EmployeeId);
            return View(training_Details);
        }

        // POST: Training_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Training_DetailsId,EmployeeId,Begin_Date,End_Date,Subject,Description")] Training_Details training_Details)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(training_Details.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Training_Details");
            }
            if (ModelState.IsValid)
            {
                db.Entry(training_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", training_Details.EmployeeId);
            return View(training_Details);
        }

        // GET: Training_Details/Delete/5
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
            Training_Details training_Details = db.Training_Details.Find(id);
            if (training_Details == null)
            {
                return HttpNotFound();
            }
            return View(training_Details);
        }

        // POST: Training_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            var c = db.Training_Details.Find(id).EmployeeId;
            if (!Session["id"].Equals(c))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Training_Details");
            }
            Training_Details training_Details = db.Training_Details.Find(id);
            db.Training_Details.Remove(training_Details);
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
