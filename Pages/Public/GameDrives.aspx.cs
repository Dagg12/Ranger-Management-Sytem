using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace TheRanger
{
    public partial class GameDrives : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) { LoadLocations(); LoadData(); } }
        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(); }
        private void LoadLocations()
        {
            string selected=ddlLocation.SelectedValue; ddlLocation.Items.Clear(); ddlLocation.Items.Add(new System.Web.UI.WebControls.ListItem("All locations",""));
            using(var con=Database.GetConnection()){con.Open(); using(var cmd=new MySqlCommand("SELECT DISTINCT location FROM GameDrive WHERE available=1 AND location IS NOT NULL AND TRIM(location)<>'' ORDER BY location",con)) using(var r=cmd.ExecuteReader()) while(r.Read()) ddlLocation.Items.Add(new System.Web.UI.WebControls.ListItem(Convert.ToString(r[0]),Convert.ToString(r[0])));}
            if(!string.IsNullOrEmpty(selected)&&ddlLocation.Items.FindByValue(selected)!=null) ddlLocation.SelectedValue=selected;
        }
        private void LoadData()
        {
            var list=new List<Item>(); string order=ddlSort.SelectedValue=="price_low"?"price ASC":ddlSort.SelectedValue=="price_high"?"price DESC":ddlSort.SelectedValue=="duration"?"duration_hours DESC":"drive_name ASC"; string search=txtSearch.Text.Trim(); string location=ddlLocation.SelectedValue??""; int guests; if(!int.TryParse(ddlGuests.SelectedValue,out guests)) guests=0;
            using(var con=Database.GetConnection()){con.Open(); string sql=@"SELECT game_drive_id,drive_name,description,location,duration_hours,price,max_guests,image_path FROM GameDrive WHERE available=1 AND (@s='' OR drive_name LIKE @t OR location LIKE @t) AND (@location='' OR location=@location) AND (@guests=0 OR max_guests>=@guests) ORDER BY "+order; using(var cmd=new MySqlCommand(sql,con)){cmd.Parameters.AddWithValue("@s",search);cmd.Parameters.AddWithValue("@t","%"+search+"%");cmd.Parameters.AddWithValue("@location",location);cmd.Parameters.AddWithValue("@guests",guests);using(var reader=cmd.ExecuteReader()) while(reader.Read()){string name=Convert.ToString(reader["drive_name"]);list.Add(new Item{game_drive_id=Convert.ToInt32(reader["game_drive_id"]),drive_name=name,description=Convert.ToString(reader["description"]),location=Convert.ToString(reader["location"]),duration_hours=Convert.ToDecimal(reader["duration_hours"]),price=Convert.ToDecimal(reader["price"]),max_guests=Convert.ToInt32(reader["max_guests"]),image_url=ImageHelper.GameDriveImage(name,Convert.ToString(reader["image_path"]))});}}}
            rptDrives.DataSource=list;rptDrives.DataBind();pnlEmpty.Visible=list.Count==0;
        }
        public class Item{public int game_drive_id{get;set;}public int max_guests{get;set;}public string drive_name{get;set;}public string description{get;set;}public string location{get;set;}public decimal duration_hours{get;set;}public decimal price{get;set;}public string image_url{get;set;}}
    }
}
