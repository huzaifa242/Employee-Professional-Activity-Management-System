using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        private ProjectData db = new ProjectData();
        // GET: Login
        public ActionResult Login()
        {
            if (TempData["login"] != null)
                ViewBag.login = TempData["login"];
            else
                ViewBag.login = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(string uname,string password)
        {
            var emp = db.employees.Where(u=>u.Name.Equals(uname)).FirstOrDefault();
            if(emp==null)
            {
                return RedirectToAction("Login", "Login");
            }
            var a = db.auths.Where(v => v.AuthId==emp.EmployeeId).First();
            if(emp.Name.Equals(uname) && a.password.Equals(password))
            {
                Session["Role"] = a.Auth_Role;
                Session["id"] = emp.EmployeeId;
                return RedirectToAction("showDetails", "Employees");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}