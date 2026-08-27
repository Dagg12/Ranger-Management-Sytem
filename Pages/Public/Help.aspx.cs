using System;
namespace TheRanger
{
    public partial class Help : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string role = Convert.ToString(Session["UserRole"]);
            pnlClientHelp.Visible = string.Equals(role, "Client", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(role);
            pnlOwnerHelp.Visible = string.Equals(role, "Owner", StringComparison.OrdinalIgnoreCase);
        }
    }
}
