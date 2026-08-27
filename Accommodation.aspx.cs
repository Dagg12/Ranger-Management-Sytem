using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace TheRanger
{
    public partial class Accommodation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { LoadLocations(); LoadData(); }
        }
        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(); }
        private void LoadLocations()
        {
            string selected = ddlLocation.SelectedValue;
            ddlLocation.Items.Clear();
            ddlLocation.Items.Add(new System.Web.UI.WebControls.ListItem("All locations", ""));
            using (var con = Database.GetConnection())
            {
                con.Open();
                using (var cmd = new MySqlCommand("SELECT DISTINCT location FROM Accommodation WHERE available=1 AND location IS NOT NULL AND TRIM(location)<>'' ORDER BY location", con))
                using (var r = cmd.ExecuteReader()) while (r.Read()) ddlLocation.Items.Add(new System.Web.UI.WebControls.ListItem(Convert.ToString(r[0]), Convert.ToString(r[0])));
            }
            if (!string.IsNullOrEmpty(selected) && ddlLocation.Items.FindByValue(selected) != null) ddlLocation.SelectedValue = selected;
        }
        private void LoadData()
        {
            var list = new List<Item>();
            string order = ddlSort.SelectedValue == "price_low" ? "price_per_night ASC" : ddlSort.SelectedValue == "price_high" ? "price_per_night DESC" : ddlSort.SelectedValue == "guests" ? "max_guests DESC" : "accommodation_name ASC";
            string search = txtSearch.Text.Trim();
            int guests; if (!int.TryParse(ddlGuests.SelectedValue, out guests)) guests = 0;
            string location = ddlLocation.SelectedValue ?? "";
            using (var con = Database.GetConnection())
            {
                con.Open();
                string sql = @"SELECT accommodation_id, accommodation_name, description, location, room_type, price_per_night, max_guests, image_path
                               FROM Accommodation WHERE available=1
                               AND (@search='' OR accommodation_name LIKE @term OR location LIKE @term OR room_type LIKE @term)
                               AND (@location='' OR location=@location) AND (@guests=0 OR max_guests>=@guests)
                               ORDER BY " + order;
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@search", search); cmd.Parameters.AddWithValue("@term", "%" + search + "%"); cmd.Parameters.AddWithValue("@location", location); cmd.Parameters.AddWithValue("@guests", guests);
                    using (var reader = cmd.ExecuteReader()) while (reader.Read())
                    {
                        string name = Convert.ToString(reader["accommodation_name"]); list.Add(new Item { accommodation_id=Convert.ToInt32(reader["accommodation_id"]), accommodation_name=name, description=Convert.ToString(reader["description"]), location=Convert.ToString(reader["location"]), room_type=Convert.ToString(reader["room_type"]), price_per_night=Convert.ToDecimal(reader["price_per_night"]), max_guests=Convert.ToInt32(reader["max_guests"]), image_url=ImageHelper.AccommodationImage(name, Convert.ToString(reader["image_path"])) });
                    }
                }
            }
            rptAccommodation.DataSource=list; rptAccommodation.DataBind(); pnlEmpty.Visible=list.Count==0;
        }
        public class Item { public int accommodation_id {get;set;} public int max_guests {get;set;} public string accommodation_name {get;set;} public string description {get;set;} public string location {get;set;} public string room_type {get;set;} public decimal price_per_night {get;set;} public string image_url {get;set;} }
    }
}
