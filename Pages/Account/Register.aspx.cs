using System;
using MySql.Data.MySqlClient;
namespace TheRanger
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text)) { ShowError("Please complete all required fields."); return; }
            if (txtPassword.Text.Length < 6) { ShowError("Password must be at least 6 characters."); return; }
            if (txtPassword.Text != txtConfirm.Text) { ShowError("Passwords do not match."); return; }
            try
            {
                using (var con = Database.GetConnection())
                {
                    con.Open();
                    using (var check = new MySqlCommand("SELECT COUNT(*) FROM Client WHERE email=@email", con)) { check.Parameters.AddWithValue("@email", txtEmail.Text.Trim()); if (Convert.ToInt32(check.ExecuteScalar()) > 0) { ShowError("An account with that email already exists."); return; } }
                    using (var cmd = new MySqlCommand("INSERT INTO Client(first_name,last_name,email,phone,password_hash) VALUES(@first,@last,@email,@phone,@hash)", con))
                    {
                        cmd.Parameters.AddWithValue("@first", txtFirstName.Text.Trim()); cmd.Parameters.AddWithValue("@last", txtLastName.Text.Trim()); cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim()); cmd.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim()); cmd.Parameters.AddWithValue("@hash", PasswordHelper.HashPassword(txtPassword.Text)); cmd.ExecuteNonQuery();
                    }
                    int id = Convert.ToInt32(new MySqlCommand("SELECT LAST_INSERT_ID()", con).ExecuteScalar());
                    Session["UserRole"] = "Client"; Session["UserId"] = id; Session["UserName"] = txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim();
                }
                Response.Redirect("ClientDashboard.aspx");
            }
            catch (Exception ex) { ShowError("Registration could not be completed. " + ex.Message); }
        }
        private void ShowError(string message) { lblMessage.CssClass = "error-text d-block mb-3"; lblMessage.Text = message; }
    }
}
