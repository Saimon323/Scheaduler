using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Scheduler.Site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SearchUsers",
                url: "Search/Users/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "SearchTasks",
                url: "Search/Tasks/{action}/{id}",
                defaults: new { controller = "Task", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "SearchMessages",
                url: "Search/Messages/{action}/{id}",
                defaults: new { controller = "Message", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Start", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}