using System;
namespace TheRanger
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string role = Session["UserRole"] == null ? "" : Session["UserRole"].ToString();
            pnlGuest.Visible = role == "";
            pnlClient.Visible = role == "Client";
            pnlOwner.Visible = role == "Owner";
            if (role != "") litAccountName.Text = Session["UserName"] == null ? role : Session["UserName"].ToString();
            litAccountName2.Text = litAccountName.Text;
        }
        protected void btnMasterLogout_Click(object sender, EventArgs e) { Session.Clear(); Session.Abandon(); Response.Redirect("Default.aspx"); }
    }
}
