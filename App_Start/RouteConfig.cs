using System.Web.Routing;

namespace TheRanger
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Keep the physical .aspx URLs working while also supporting the
            // clean URLs used by the navigation (for example /Login).
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("Root", "", "~/Default.aspx");
            routes.MapPageRoute("Home", "Home", "~/Default.aspx");
            routes.MapPageRoute("Default", "Default", "~/Default.aspx");
            routes.MapPageRoute("Login", "Login", "~/Login.aspx");
            routes.MapPageRoute("Register", "Register", "~/Register.aspx");
            routes.MapPageRoute("Accommodation", "Accommodation", "~/Accommodation.aspx");
            routes.MapPageRoute("GameDrives", "GameDrives", "~/GameDrives.aspx");
            routes.MapPageRoute("ClientDashboard", "ClientDashboard", "~/ClientDashboard.aspx");
            routes.MapPageRoute("OwnerDashboard", "OwnerDashboard", "~/OwnerDashboard.aspx");
            routes.MapPageRoute("OwnerAccommodation", "OwnerAccommodation", "~/OwnerAccommodation.aspx");
            routes.MapPageRoute("OwnerGameDrives", "OwnerGameDrives", "~/OwnerGameDrives.aspx");
            routes.MapPageRoute("OwnerBookings", "OwnerBookings", "~/OwnerBookings.aspx");
            routes.MapPageRoute("About", "About", "~/About.aspx");
            routes.MapPageRoute("Contact", "Contact", "~/Contact.aspx");
        }
    }
}
