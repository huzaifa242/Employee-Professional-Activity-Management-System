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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project.Controllers
{
    public class EmployeesController : Controller
    {
        private ProjectData db = new ProjectData();

        // GET: Employees
        public ActionResult Index()
        {
            if(TempData["auth"]!=null)
                ViewBag.auth = TempData["auth"];
            else
                ViewBag.auth = "";
            var employees = db.employees.Include(e => e.auth);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.auths, "AuthId", "password");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,Name,Dob,Permanent_Addr,Current_Addr,Marital_Status,Gender,Contact_No,Email,Blood_Group")] Employee employee,string password)
        {
            var f = 0;
            ViewBag.error = "";
            if (employee.Contact_No.Length != 10)
            {
                ViewBag.error += "Invalid Contact-No.";
                f = 1;
            }
            if (employee.Name.Length > 15)
            {
                f = 1;
                ViewBag.error += "  Name length must be less than 16.";
            }
            if (!employee.Gender.ToLower().Equals("male") && !employee.Gender.ToLower().Equals("female"))
            {
                f = 1;
                ViewBag.error += "  Invalid Gender.";
            }
            if (ModelState.IsValid && f==0)
            {
                string path = @"C:\Users\Kishan\source\repos\Project\Project\directory\" +employee.Name;
                if (!(Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                    string path1;
                    path1 = path + @"\" + "Educational_Certificates";
                    Directory.CreateDirectory(path1);
                    path1 = path + @"\" + "Workshop_Certificates";
                    Directory.CreateDirectory(path1);
                    path1 = path + @"\" + "Training_Certificates";
                    Directory.CreateDirectory(path1);
                }
                path= @"C:\Users\Kishan\source\repos\Project\Project\confirmation_certificate\" + employee.Name;
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
                Auth a = new Auth();
                a.AuthId = employee.EmployeeId;
                a.Auth_Role = "user";
                a.password = password;
                db.auths.Add(a);
                db.employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.auths, "AuthId", "password", employee.EmployeeId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if(!Session["id"].Equals(id))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.auths, "AuthId", "password", employee.EmployeeId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,Name,Dob,Permanent_Addr,Current_Addr,Marital_Status,Gender,Contact_No,Email,Blood_Group")] Employee employee)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(employee.EmployeeId))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            var f = 0;
            if (employee.Contact_No.Length != 10)
            {
                ViewBag.error += "Invalid Contact-No.";
                f = 1;
            }
            if (employee.Name.Length > 15)
            {
                f = 1;
                ViewBag.error += "  Name length must be less than 16.";
            }
            if (!employee.Gender.ToLower().Equals("male") && !employee.Gender.ToLower().Equals("female"))
            {
                f = 1;
                ViewBag.error += "  Invalid Gender.";
            }
            if (ModelState.IsValid && f==0)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.auths, "AuthId", "password", employee.EmployeeId);
            return View(employee);
        }

        // GET: Employees/Delete/5
      /*  public ActionResult Delete(int? id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(id))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }*/
        public ActionResult showDetails()
        {
            if(Session["id"]==null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public PartialViewResult RenderEmp()
        {
            Employee emp = db.employees.Find(Session["id"]);
            return PartialView(emp);
        }
        public PartialViewResult RenderEdu()
        {
            int id = (Int32)(Session["id"]);
            var edu = db.Educational_details.Where(u=>u.EmployeeId==id);
            return PartialView(edu.ToList());
        }
        public PartialViewResult RenderWrok()
        {
            int id = (Int32)(Session["id"]);
            var edu = db.Workshop_Details.Where(u => u.EmployeeId == id);
            return PartialView(edu.ToList());
        }
        public PartialViewResult RenderTraining()
        {
            int id = (Int32)(Session["id"]);
            var edu = db.Training_Details.Where(u => u.EmployeeId == id);
            return PartialView(edu.ToList());
        }
        public PartialViewResult RenderProject()
        {
            int id = (Int32)(Session["id"]);
            var edu = db.Turnkey_Project.Where(u => u.EmployeeId == id);
            return PartialView(edu.ToList());
        }
        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        /*public ActionResult DeleteConfirmed(int id)
        {
            if (Session["id"] == null)
            {
                TempData["login"] = "please login first";
                return RedirectToAction("Login", "Login");
            }
            if (!Session["id"].Equals(id))
            {
                TempData["auth"] = "You are not Authorized so move back";
                return RedirectToAction("Index", "Employees");
            }
            Employee employee = db.employees.Find(id);
            Educational_details ed;
            if((ed=db.Educational_details.Find(id))!=null)
            {
                db.Educational_details.Remove(ed);
            }
            Training_Details td;
            if((td = db.Training_Details.Find(id))!=null)
            {
                db.Training_Details.Remove(td);
            }
            Auth a;
            if((a= db.auths.Find(id))!=null)
            {
                db.auths.Remove(a);
            }
            Turnkey_Project tp; 
            if((tp= db.Turnkey_Project.Find(id))!=null)
            {
                db.Turnkey_Project.Remove(tp);
            }
            Workshop_Details wd;
            if((wd= db.Workshop_Details.Find(id))!=null)
            {
                db.Workshop_Details.Remove(wd);
            }
            Confirmation c;
            if((c= db.Confirmations.Find(id))!=null)
            {
                db.Confirmations.Remove(c);
            }
            db.employees.Remove(employee);
            string path = @"C:\Users\Kishan\source\repos\Project\Project\directory\" + employee.Name;
            string path1;
            path1= path + @"\" + "Educational_Certificates";
            DirectoryInfo di;
            di= new DirectoryInfo(path1);
            foreach (FileInfo filename in di.GetFiles())
            {
                filename.Delete();               
            }
            Directory.Delete(path1);
            path1 = path + @"\" + "Workshop_Certificates";
            di = new DirectoryInfo(path1);
            foreach (FileInfo filename in di.GetFiles())
            {
                filename.Delete();
            }
            Directory.Delete(path1);
            path1 = path + @"\" + "Training_Certificates";
            di = new DirectoryInfo(path1);
            foreach (FileInfo filename in di.GetFiles())
            {
                filename.Delete();
            }
            Directory.Delete(path1);
            Directory.Delete(path);
            path = @"C:\Users\Kishan\source\repos\Project\Project\confirmation_certificate\" + employee.Name;
            path1 = path + @"\" + "Educational";
            di = new DirectoryInfo(path1);
            foreach (FileInfo filename in di.GetFiles())
            {
                filename.Delete();
            }
            Directory.Delete(path1);
            path1 = path + @"\" + "Workshop";
            di = new DirectoryInfo(path1);
            foreach (FileInfo filename in di.GetFiles())
            {
                filename.Delete();
            }
            Directory.Delete(path1);
            path1 = path + @"\" + "Training";
            di = new DirectoryInfo(path1);
            foreach (FileInfo filename in di.GetFiles())
            {
                filename.Delete();
            }
            Directory.Delete(path1);
            Directory.Delete(path);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
