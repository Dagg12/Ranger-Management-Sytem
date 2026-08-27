using System;
using MySql.Data.MySqlClient;
namespace TheRanger
{
    public partial class AccommodationBooking : System.Web.UI.Page
    {
        private int AccommodationId { get { int id; return int.TryParse(Request.QueryString["id"], out id) ? id : 0; } }
        public decimal Price { get; private set; } private int max;
        public string ImageUrl { get; private set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AccommodationId <= 0) { Response.Redirect("Accommodation.aspx"); return; }
            if (!IsClient()) { pnlLogin.Visible=true; pnlBooking.Visible=false; return; }
            LoadItem();
        }
        private bool IsClient() { return Session["UserRole"] != null && string.Equals(Session["UserRole"].ToString(),"Client",StringComparison.Ordinal); }
        private void LoadItem()
        {
            using(var c=Database.GetConnection()){c.Open();using(var cmd=new MySqlCommand("SELECT accommodation_name,description,location,price_per_night,max_guests,image_path FROM Accommodation WHERE accommodation_id=@id AND available=1",c)){cmd.Parameters.AddWithValue("@id",AccommodationId);using(var r=cmd.ExecuteReader()){if(!r.Read()){Response.Redirect("Accommodation.aspx");return;}string name=Convert.ToString(r["accommodation_name"]);ImageUrl=ImageHelper.AccommodationImage(name,Convert.ToString(r["image_path"]));litName.Text=Server.HtmlEncode(name);litDescription.Text=Server.HtmlEncode(Convert.ToString(r["description"]));litLocation.Text=Server.HtmlEncode(Convert.ToString(r["location"]));Price=Convert.ToDecimal(r["price_per_night"]);max=Convert.ToInt32(r["max_guests"]);litPrice.Text=Price.ToString("N2");litGuests.Text=max.ToString();txtGuests.Attributes["min"]="1";txtGuests.Attributes["max"]=max.ToString();txtCheckIn.Attributes["min"]=DateTime.Today.ToString("yyyy-MM-dd");txtCheckOut.Attributes["min"]=DateTime.Today.ToString("yyyy-MM-dd");}}}}
        protected void btnBook_Click(object sender,EventArgs e)
        {
            if(!IsClient()){Response.Redirect("Login.aspx");return;}
            DateTime ci,co; if(!DateTime.TryParse(txtCheckIn.Text,out ci)||!DateTime.TryParse(txtCheckOut.Text,out co)||co<=ci){Err("Please enter valid check-in and check-out dates.");return;} if(ci.Date<DateTime.Today){Err("Check-in cannot be in the past.");return;}
            int guests; if(!int.TryParse(txtGuests.Text,out guests)||guests<1){Err("Enter a valid number of guests.");return;}
            using(var c=Database.GetConnection()){c.Open();
                using(var info=new MySqlCommand("SELECT price_per_night,max_guests,available FROM Accommodation WHERE accommodation_id=@id",c)){info.Parameters.AddWithValue("@id",AccommodationId);using(var r=info.ExecuteReader()){if(!r.Read()){Err("Accommodation is no longer available.");return;}Price=Convert.ToDecimal(r["price_per_night"]);max=Convert.ToInt32(r["max_guests"]);if(!Convert.ToBoolean(r["available"])||guests>max){Err("This accommodation is not available for that number of guests.");return;}}}
                using(var overlap=new MySqlCommand("SELECT COUNT(*) FROM Bookings WHERE accommodation_id=@id AND booking_status IN('Pending','Confirmed') AND check_in_date < @out AND check_out_date > @in",c)){overlap.Parameters.AddWithValue("@id",AccommodationId);overlap.Parameters.AddWithValue("@in",ci.Date);overlap.Parameters.AddWithValue("@out",co.Date);if(Convert.ToInt32(overlap.ExecuteScalar())>0){Err("Those dates are already booked. Please choose different dates.");return;}}
                decimal total=(co.Date-ci.Date).Days*Price;
                using(var cmd=new MySqlCommand("INSERT INTO Bookings(client_id,accommodation_id,check_in_date,check_out_date,number_of_guests,total_amount,booking_status) VALUES(@c,@a,@in,@out,@g,@t,'Pending')",c)){cmd.Parameters.AddWithValue("@c",Convert.ToInt32(Session["UserId"]));cmd.Parameters.AddWithValue("@a",AccommodationId);cmd.Parameters.AddWithValue("@in",ci.Date);cmd.Parameters.AddWithValue("@out",co.Date);cmd.Parameters.AddWithValue("@g",guests);cmd.Parameters.AddWithValue("@t",total);cmd.ExecuteNonQuery();}
            }
            Response.Redirect("ClientDashboard.aspx",false); Context.ApplicationInstance.CompleteRequest();
        }
        private void Err(string x){lblMessage.CssClass="alert alert-danger";lblMessage.Text=Server.HtmlEncode(x);}
    }
}
