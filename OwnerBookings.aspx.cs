using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace TheRanger
{
    public partial class OwnerBookings : System.Web.UI.Page
    {
        int OwnerId { get { return Convert.ToInt32(Session["UserId"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsOwner()) { Response.Redirect("Login.aspx"); return; }
            if (!IsPostBack)
            {
                LoadLocations();
                LoadData();
            }
        }

        bool IsOwner() { return Session["UserRole"] != null && Session["UserRole"].ToString() == "Owner"; }

        void LoadLocations()
        {
            string selected = ddlLocation.SelectedValue;
            ddlLocation.Items.Clear();
            ddlLocation.Items.Add(new ListItem("All locations", ""));
            using (var c = Database.GetConnection())
            {
                c.Open();
                using (var cmd = new MySqlCommand(@"SELECT location FROM (
                    SELECT DISTINCT location FROM Accommodation WHERE owner_id=@o
                    UNION
                    SELECT DISTINCT location FROM GameDrive WHERE owner_id=@o
                ) x WHERE location IS NOT NULL AND TRIM(location)<>'' ORDER BY location", c))
                {
                    cmd.Parameters.AddWithValue("@o", OwnerId);
                    using (var r = cmd.ExecuteReader())
                        while (r.Read()) ddlLocation.Items.Add(new ListItem(r[0].ToString(), r[0].ToString()));
                }
            }
            if (ddlLocation.Items.FindByValue(selected) != null) ddlLocation.SelectedValue = selected;
        }

        List<Item> GetData()
        {
            var list = new List<Item>();
            var where = new StringBuilder(" WHERE 1=1 ");
            string search = txtSearch.Text.Trim();
            DateTime from, to;

            if (!string.IsNullOrEmpty(search)) where.Append(" AND (client_name LIKE @search OR email LIKE @search OR item_name LIKE @search OR location LIKE @search) ");
            if (!string.IsNullOrEmpty(ddlType.SelectedValue)) where.Append(" AND booking_type=@type ");
            if (!string.IsNullOrEmpty(ddlReportType.SelectedValue) && (ddlReportType.SelectedValue == "Accommodation" || ddlReportType.SelectedValue == "Game Drive")) where.Append(" AND booking_type=@reportType ");
            if (!string.IsNullOrEmpty(ddlReportType.SelectedValue) && (ddlReportType.SelectedValue == "Pending" || ddlReportType.SelectedValue == "Confirmed" || ddlReportType.SelectedValue == "Completed" || ddlReportType.SelectedValue == "Cancelled")) where.Append(" AND booking_status=@reportStatus ");
            if (!string.IsNullOrEmpty(ddlLocation.SelectedValue)) where.Append(" AND location=@location ");
            if (!string.IsNullOrEmpty(ddlStatus.SelectedValue)) where.Append(" AND booking_status=@status ");
            if (DateTime.TryParse(txtFrom.Text, out from)) where.Append(" AND DATE(booking_date)>=@from ");
            if (DateTime.TryParse(txtTo.Text, out to)) where.Append(" AND DATE(booking_date)<=@to ");

            string q = @"SELECT booking_id,booking_type,client_name,email,item_name,location,date_display,number_of_guests,total_amount,booking_status,booking_date FROM(
                SELECT b.booking_id,'Accommodation' booking_type,CONCAT(cl.first_name,' ',cl.last_name) client_name,cl.email,
                       a.accommodation_name item_name,a.location,
                       CONCAT(DATE_FORMAT(b.check_in_date,'%d %b %Y'),' - ',DATE_FORMAT(b.check_out_date,'%d %b %Y')) date_display,
                       b.number_of_guests,b.total_amount,b.booking_status,b.booking_date
                FROM Bookings b JOIN Client cl ON cl.client_id=b.client_id JOIN Accommodation a ON a.accommodation_id=b.accommodation_id
                WHERE a.owner_id=@o
                UNION ALL
                SELECT d.drive_booking_id,'Game Drive',CONCAT(cl.first_name,' ',cl.last_name),cl.email,
                       g.drive_name,g.location,DATE_FORMAT(d.drive_date,'%d %b %Y'),
                       d.number_of_guests,d.total_amount,d.booking_status,d.booking_date
                FROM DriveBookings d JOIN Client cl ON cl.client_id=d.client_id JOIN GameDrive g ON g.game_drive_id=d.game_drive_id
                WHERE g.owner_id=@o
            ) x" + where + " ORDER BY booking_date DESC";

            using (var c = Database.GetConnection())
            {
                c.Open();
                using (var cmd = new MySqlCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@o", OwnerId);
                    if (!string.IsNullOrEmpty(search)) cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    if (!string.IsNullOrEmpty(ddlType.SelectedValue)) cmd.Parameters.AddWithValue("@type", ddlType.SelectedValue);
                    if (!string.IsNullOrEmpty(ddlReportType.SelectedValue) && (ddlReportType.SelectedValue == "Accommodation" || ddlReportType.SelectedValue == "Game Drive")) cmd.Parameters.AddWithValue("@reportType", ddlReportType.SelectedValue);
                    if (!string.IsNullOrEmpty(ddlReportType.SelectedValue) && (ddlReportType.SelectedValue == "Pending" || ddlReportType.SelectedValue == "Confirmed" || ddlReportType.SelectedValue == "Completed" || ddlReportType.SelectedValue == "Cancelled")) cmd.Parameters.AddWithValue("@reportStatus", ddlReportType.SelectedValue);
                    if (!string.IsNullOrEmpty(ddlLocation.SelectedValue)) cmd.Parameters.AddWithValue("@location", ddlLocation.SelectedValue);
                    if (!string.IsNullOrEmpty(ddlStatus.SelectedValue)) cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
                    if (DateTime.TryParse(txtFrom.Text, out from)) cmd.Parameters.AddWithValue("@from", from.Date);
                    if (DateTime.TryParse(txtTo.Text, out to)) cmd.Parameters.AddWithValue("@to", to.Date);

                    using (var r = cmd.ExecuteReader())
                        while (r.Read()) list.Add(new Item
                        {
                            booking_id = Convert.ToInt32(r["booking_id"]),
                            booking_type = r["booking_type"].ToString(),
                            client_name = r["client_name"].ToString(),
                            email = r["email"].ToString(),
                            item_name = r["item_name"].ToString(),
                            location = r["location"].ToString(),
                            date_display = r["date_display"].ToString(),
                            number_of_guests = Convert.ToInt32(r["number_of_guests"]),
                            total_amount = Convert.ToDecimal(r["total_amount"]),
                            booking_status = r["booking_status"].ToString(),
                            booking_date_display = Convert.ToDateTime(r["booking_date"]).ToString("dd MMM yyyy HH:mm")
                        });
                }
            }
            return list;
        }

        void Bind(List<Item> list)
        {
            rptBookings.DataSource = list;
            rptBookings.DataBind();
            pnlEmpty.Visible = list.Count == 0;
            litResultCount.Text = list.Count.ToString("N0");
        }

        void LoadData() { Bind(GetData()); }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            pnlReportPreview.Visible = false;
            LoadData();
        }

        protected void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Text = txtFrom.Text = txtTo.Text = "";
            ddlType.SelectedIndex = 0;
            ddlLocation.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlReportType.SelectedIndex = 0;
            pnlReportPreview.Visible = false;
            LoadData();
        }

        protected void btnPreviewReport_Click(object sender, EventArgs e)
        {
            var list = GetData();
            rptPreview.DataSource = list;
            rptPreview.DataBind();
            litReportTitle.Text = Server.HtmlEncode(GetReportTitle());
            litPreviewCount.Text = list.Count.ToString("N0");
            litPreviewTotal.Text = SumTotal(list).ToString("N2");
            pnlReportPreview.Visible = true;
            pnlEmptyPreview.Visible = list.Count == 0;
        }

        protected void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            var list = GetData();
            var rows = new List<PdfReportHelper.Row>();
            foreach (var x in list) rows.Add(new PdfReportHelper.Row
            {
                Id = x.booking_id.ToString(), Type = x.booking_type, Guest = x.client_name, Experience = x.item_name,
                Location = x.location, Date = x.date_display, Guests = x.number_of_guests.ToString(),
                Total = x.total_amount.ToString("N2"), Status = x.booking_status, BookedOn = x.booking_date_display
            });
            byte[] pdf = PdfReportHelper.BuildBookingsReport(GetReportTitle(), Convert.ToString(Session["UserName"]), rows);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=The-Ranger-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".pdf");
            Response.OutputStream.Write(pdf, 0, pdf.Length);
            Response.Flush();
            Context.ApplicationInstance.CompleteRequest();
        }

        string GetReportTitle()
        {
            switch (ddlReportType.SelectedValue)
            {
                case "Accommodation": return "Accommodation Bookings Report";
                case "Game Drive": return "Game Drive Bookings Report";
                case "Pending": return "Pending Bookings Report";
                case "Confirmed": return "Confirmed Bookings Report";
                case "Completed": return "Completed Bookings Report";
                case "Cancelled": return "Cancelled Bookings Report";
                default: return "Safari Bookings Report";
            }
        }

        decimal SumTotal(List<Item> list) { decimal total = 0; foreach (var x in list) total += x.total_amount; return total; }

        protected void Update_Command(object s, CommandEventArgs e)
        {
            string[] p = e.CommandArgument.ToString().Split('|');
            int id = Convert.ToInt32(p[0]); string type = p[1];
            var row = (RepeaterItem)((System.Web.UI.Control)s).NamingContainer;
            var ddl = (DropDownList)row.FindControl("ddlRowStatus");
            using (var c = Database.GetConnection())
            {
                c.Open();
                string q = type == "Accommodation"
                    ? "UPDATE Bookings b JOIN Accommodation a ON a.accommodation_id=b.accommodation_id SET b.booking_status=@s WHERE b.booking_id=@id AND a.owner_id=@o"
                    : "UPDATE DriveBookings d JOIN GameDrive g ON g.game_drive_id=d.game_drive_id SET d.booking_status=@s WHERE d.drive_booking_id=@id AND g.owner_id=@o";
                using (var cmd = new MySqlCommand(q, c))
                {
                    cmd.Parameters.AddWithValue("@s", ddl.SelectedValue); cmd.Parameters.AddWithValue("@id", id); cmd.Parameters.AddWithValue("@o", OwnerId); cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            lblMessage.CssClass = "alert alert-success d-block";
            lblMessage.Text = "Booking status updated.";
        }

        class Item
        {
            public int booking_id { get; set; }
            public int number_of_guests { get; set; }
            public string booking_type { get; set; }
            public string client_name { get; set; }
            public string email { get; set; }
            public string item_name { get; set; }
            public string location { get; set; }
            public string date_display { get; set; }
            public string booking_status { get; set; }
            public decimal total_amount { get; set; }
            public string booking_date_display { get; set; }
        }
    }
}
