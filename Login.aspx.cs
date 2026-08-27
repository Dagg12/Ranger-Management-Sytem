using System;
using MySql.Data.MySqlClient;

namespace TheRanger
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["UserRole"] != null) RedirectByRole();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim(); string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) { lblMessage.Text = "Please enter your email and password."; return; }
            try
            {
                using (var con = Database.GetConnection())
                {
                    con.Open();
                    if (TryLogin(con, "Client", "client_id", email, password, out int id, out string name))
                    { SetSession("Client", id, name); Response.Redirect("ClientDashboard.aspx"); return; }
                    if (TryLogin(con, "Owner", "owner_id", email, password, out id, out name))
                    { SetSession("Owner", id, name); Response.Redirect("OwnerDashboard.aspx"); return; }
                }
                lblMessage.Text = "Invalid email or password.";
            }
            catch (Exception ex) { lblMessage.Text = "We could not connect to The Ranger database. Check MySQL is running and your Web.config password is correct. " + ex.Message; }
        }
        private void SetSession(string role, int id, string name) { Session["UserRole"] = role; Session["UserId"] = id; Session["UserName"] = name; }
        private void RedirectByRole() { Response.Redirect(Session["UserRole"].ToString() == "Owner" ? "OwnerDashboard.aspx" : "ClientDashboard.aspx"); }
        private bool TryLogin(MySqlConnection con, string table, string idColumn, string email, string password, out int id, out string name)
        {
            id = 0; name = "";
            using (var cmd = new MySqlCommand("SELECT " + idColumn + ",first_name,last_name,password_hash FROM " + table + " WHERE email=@email LIMIT 1", con))
            {
                cmd.Parameters.AddWithValue("@email", email);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read() || !PasswordHelper.VerifyPassword(password, reader["password_hash"].ToString())) return false;
                    id = Convert.ToInt32(reader[idColumn]); name = reader["first_name"] + " " + reader["last_name"]; return true;
                }
            }
        }
    }
}
