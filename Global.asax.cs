using System;
using System.Web;
using System.Web.Routing;

namespace TheRanger
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Register clean application routes before the first request.
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Database seeding is intentionally best-effort. The application
            // must still start if MySQL is temporarily unavailable.
            try
            {
                DatabaseInitializer.EnsureSeedData();
            }
            catch
            {
                // Individual pages will show the database connection error if
                // the database is unavailable when a user requests data.
            }
        }
    }
}
