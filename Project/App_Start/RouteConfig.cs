using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                 name: "Employee",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Employees", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                 name: "Auth",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Auths", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                 name: "Education",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Educational_details", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                 name: "Workshop",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Workshop_Details", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                 name: "Training",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Training_Details", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                 name: "Turnkey",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Turnkey_Project", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                 name: "Confirm",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Confirmations", action = "Index", id = UrlParameter.Optional }
             );
        }
    }
}
