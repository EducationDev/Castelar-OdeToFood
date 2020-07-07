using OdeToFood.WebSite.Services;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OdeToFood.WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        protected void Application_Error(object sender, EventArgs e)
        {
            // Obtener la última excepción
            var exception = HttpContext.Current.Server.GetLastError();

            if (exception != null)
                Logger.Instance.LogException(exception);
        }
        protected void Application_End(object sender, EventArgs e)
        {
            // Codigo que se ejectura cuando finaliza la aplicacion.
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            var cookie = HelperCookie.CookieExist("shop-art", "shop-art-key");
            if (!cookie)
                HelperCookie.StoreInCookie("shop-art", "shop-art-key", "shop-art-2020", new DateTime(2020, 8, 1));
        }
        protected void Session_End(object sender, EventArgs e)
        {
            // Codigo que se ejectura cuando finaliza la sesion.
        }


    }
}
