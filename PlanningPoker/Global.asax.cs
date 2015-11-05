using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PlanningPoker.Models;

namespace PlanningPoker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var game = new PokerGame("Test Game", "Michael Kaltner", "Test Description", "10711001-8c24-4917-8537-df2d49d0e36b");
            GameManager.StorePokerGame(game);
        }
    }
}
