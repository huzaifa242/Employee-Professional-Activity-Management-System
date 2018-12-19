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
    public class Workshop_DetailsController : Controller
    {
        private ProjectData db = new ProjectData();

        // GET: Workshop_Details
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
            var workshop_Details = db.Workshop_Details.Include(w => w.employee);
            return View(workshop_Details.ToList());
        }

        // GET: Workshop_Details/Details/5
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
            Workshop_Details workshop_Details = db.Workshop_Details.Find(id);
            if (workshop_Details == null)
            {
                return HttpNotFound();
            }
            return View(workshop_Details);
        }

        // GET: Workshop_Details/Create
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

        // POST: Workshop_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Workshop_DetailsId,EmployeeId,Begin_Date,End_Date,Subject,Description,Audience_Count")] Workshop_Details workshop_Details)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(workshop_Details.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Workshop_Details");
            }
            if (ModelState.IsValid)
            {
                db.Workshop_Details.Add(workshop_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", workshop_Details.EmployeeId);
            return View(workshop_Details);
        }

        // GET: Workshop_Details/Edit/5
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
            Workshop_Details workshop_Details = db.Workshop_Details.Find(id);
            if (workshop_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", workshop_Details.EmployeeId);
            return View(workshop_Details);
        }

        // POST: Workshop_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Workshop_DetailsId,EmployeeId,Begin_Date,End_Date,Subject,Description,Audience_Count")] Workshop_Details workshop_Details)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(workshop_Details.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Workshop_Details");
            }
            if (ModelState.IsValid)
            {
                db.Entry(workshop_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.employees, "EmployeeId", "Name", workshop_Details.EmployeeId);
            return View(workshop_Details);
        }

        // GET: Workshop_Details/Delete/5
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
            Workshop_Details workshop_Details = db.Workshop_Details.Find(id);
            if (workshop_Details == null)
            {
                return HttpNotFound();
            }
            return View(workshop_Details);
        }

        // POST: Workshop_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            var c = db.Workshop_Details.Find(id).EmployeeId;
            if (!Session["id"].Equals(c))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Workshop_Details");
            }
            Workshop_Details workshop_Details = db.Workshop_Details.Find(id);
            db.Workshop_Details.Remove(workshop_Details);
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
