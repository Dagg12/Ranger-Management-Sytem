using System;
using MySql.Data.MySqlClient;
namespace TheRanger
{
    public partial class GameDriveBooking : System.Web.UI.Page
    {
        private int GameDriveId { get { int id; return int.TryParse(Request.QueryString["id"], out id) ? id : 0; } }
        public decimal Price { get; private set; } private int max;
        public string ImageUrl { get; private set; }
        protected void Page_Load(object sender,EventArgs e)
        {
            if(GameDriveId<=0){Response.Redirect("GameDrives.aspx");return;}
            if(!IsClient()){pnlLogin.Visible=true;pnlBooking.Visible=false;return;}
            LoadItem();
        }
        private bool IsClient(){return Session["UserRole"]!=null&&string.Equals(Session["UserRole"].ToString(),"Client",StringComparison.Ordinal);}
        private void LoadItem(){using(var c=Database.GetConnection()){c.Open();using(var cmd=new MySqlCommand("SELECT drive_name,description,location,duration_hours,price,max_guests,image_path FROM GameDrive WHERE game_drive_id=@id AND available=1",c)){cmd.Parameters.AddWithValue("@id",GameDriveId);using(var r=cmd.ExecuteReader()){if(!r.Read()){Response.Redirect("GameDrives.aspx");return;}string name=Convert.ToString(r["drive_name"]);ImageUrl=ImageHelper.GameDriveImage(name,Convert.ToString(r["image_path"]));litName.Text=Server.HtmlEncode(name);litDescription.Text=Server.HtmlEncode(Convert.ToString(r["description"]));litLocation.Text=Server.HtmlEncode(Convert.ToString(r["location"]));Price=Convert.ToDecimal(r["price"]);max=Convert.ToInt32(r["max_guests"]);litPrice.Text=Price.ToString("N2");litGuests.Text=max.ToString();litDuration.Text=Convert.ToDecimal(r["duration_hours"]).ToString("0.##");txtGuests.Attributes["min"]="1";txtGuests.Attributes["max"]=max.ToString();txtDate.Attributes["min"]=DateTime.Today.ToString("yyyy-MM-dd");}}}}
        protected void btnBook_Click(object sender,EventArgs e){if(!IsClient()){Response.Redirect("Login.aspx");return;}DateTime date;if(!DateTime.TryParse(txtDate.Text,out date)||date.Date<DateTime.Today){Err("Please choose a valid future date.");return;}int guests;if(!int.TryParse(txtGuests.Text,out guests)||guests<1){Err("Enter a valid number of guests.");return;}using(var c=Database.GetConnection()){c.Open();using(var info=new MySqlCommand("SELECT price,max_guests,available FROM GameDrive WHERE game_drive_id=@id",c)){info.Parameters.AddWithValue("@id",GameDriveId);using(var r=info.ExecuteReader()){if(!r.Read()){Err("Drive is no longer available.");return;}Price=Convert.ToDecimal(r["price"]);max=Convert.ToInt32(r["max_guests"]);if(!Convert.ToBoolean(r["available"])||guests>max){Err("This drive cannot accept that many guests.");return;}}}using(var cap=new MySqlCommand("SELECT COALESCE(SUM(number_of_guests),0) FROM DriveBookings WHERE game_drive_id=@id AND drive_date=@date AND booking_status IN('Pending','Confirmed')",c)){cap.Parameters.AddWithValue("@id",GameDriveId);cap.Parameters.AddWithValue("@date",date.Date);int used=Convert.ToInt32(cap.ExecuteScalar());if(used+guests>max){Err("That drive is full on the selected date. Please choose another date.");return;}}decimal total=guests*Price;using(var cmd=new MySqlCommand("INSERT INTO DriveBookings(client_id,game_drive_id,drive_date,number_of_guests,total_amount,booking_status) VALUES(@c,@d,@date,@g,@t,'Pending')",c)){cmd.Parameters.AddWithValue("@c",Convert.ToInt32(Session["UserId"]));cmd.Parameters.AddWithValue("@d",GameDriveId);cmd.Parameters.AddWithValue("@date",date.Date);cmd.Parameters.AddWithValue("@g",guests);cmd.Parameters.AddWithValue("@t",total);cmd.ExecuteNonQuery();}}Response.Redirect("ClientDashboard.aspx",false);Context.ApplicationInstance.CompleteRequest();}
        private void Err(string x){lblMessage.CssClass="alert alert-danger";lblMessage.Text=Server.HtmlEncode(x);}
    }
}
