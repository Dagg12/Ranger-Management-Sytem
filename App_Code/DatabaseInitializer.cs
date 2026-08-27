using System;
using MySql.Data.MySqlClient;

namespace TheRanger
{
    /// <summary>
    /// Adds safe development starter data when the database is empty.
    /// It does not create or alter tables.
    /// </summary>
    public static class DatabaseInitializer
    {
        private static readonly StarterDrive[] StarterDrives =
        {
            new StarterDrive("Sunrise Wildlife Drive", "Early morning guided drive with excellent opportunities to see predators and grazing herds.", "Kruger Wilderness", 3.00m, 850.00m, 6, "Images/Sunrise Wildlife Drive.jpg"),
            new StarterDrive("Golden Hour Safari", "A relaxed late-afternoon drive timed for golden light, wildlife activity and spectacular bush sunsets.", "Lowveld Bush", 3.50m, 950.00m, 6, "Images/Golden Hour Safari.jpg"),
            new StarterDrive("Big Five Adventure", "A full safari experience focused on tracking the Big Five with an experienced ranger.", "Greater Kruger", 5.00m, 1450.00m, 6, "Images/Big Five Adventure.jpg"),
            new StarterDrive("Bushveld Discovery Drive", "A family-friendly wildlife drive exploring tracks, birds and the smaller stories of the bush.", "Sabie Valley", 2.50m, 700.00m, 8, "Images/Bushveld Discovery Drive.png"),
            new StarterDrive("Night Safari Experience", "An after-dark safari using specialist lighting to discover nocturnal wildlife in the reserve.", "Private Game Reserve", 3.00m, 1100.00m, 6, "Images/Night Safari Experience.jpg")
        };

        private static readonly StarterAccommodation[] StarterAccommodations =
        {
            new StarterAccommodation("Impala Safari Camp", "Comfortable safari tents surrounded by bushveld, with easy access to guided wildlife experiences.", "Kruger Wilderness", "Luxury Safari Tent", 1650.00m, 2, "Images/A1.jpg"),
            new StarterAccommodation("Mdonya Old River Camp", "A peaceful riverside camp offering an authentic wilderness stay and spacious guest tents.", "Sabie Valley", "River Tent", 1850.00m, 3, "Images/A2.jpg"),
            new StarterAccommodation("Golden Glamping Retreat", "Stylish glamping accommodation with a relaxed outdoor atmosphere and beautiful sunset views.", "Lowveld Bush", "Glamping Tent", 2100.00m, 2, "Images/A3.jpg"),
            new StarterAccommodation("Ranger Luxury Tented Lodge", "Premium safari tents with comfortable furnishings and a private wilderness feel.", "Greater Kruger", "Luxury Tent", 2450.00m, 4, "Images/Luxury Tent.avif"),
            new StarterAccommodation("Bushveld Romantic Chalet", "A cosy private chalet designed for couples looking for a quiet safari escape.", "Private Game Reserve", "Safari Chalet", 2300.00m, 2, "Images/Safari Chalet.webp"),
            new StarterAccommodation("Wilderness Family Camp", "Spacious family-friendly accommodation that makes a comfortable base for your safari adventure.", "Kruger Wilderness", "Family Safari Tent", 2800.00m, 6, "Images/Family Safari Tent.png")
        };

        public static void EnsureSeedData()
        {
            using (var con = Database.GetConnection())
            {
                con.Open();
                int ownerId = EnsureDevelopmentOwner(con);
                EnsureStarterDrives(con, ownerId);
                EnsureStarterAccommodations(con, ownerId);
                EnsureStarterImagePaths(con);
            }
        }

        private static int EnsureDevelopmentOwner(MySqlConnection con)
        {
            using (var find = new MySqlCommand("SELECT owner_id FROM Owner ORDER BY owner_id LIMIT 1", con))
            {
                object value = find.ExecuteScalar();
                if (value != null && value != DBNull.Value) return Convert.ToInt32(value);
            }

            using (var insert = new MySqlCommand(
                "INSERT INTO Owner(first_name,last_name,email,phone,password_hash) VALUES(@f,@l,@e,@p,@h)", con))
            {
                insert.Parameters.AddWithValue("@f", "Ranger");
                insert.Parameters.AddWithValue("@l", "Manager");
                insert.Parameters.AddWithValue("@e", "owner@theranger.co.za");
                insert.Parameters.AddWithValue("@p", "0600000000");
                insert.Parameters.AddWithValue("@h", PasswordHelper.HashPassword("Ranger123"));
                insert.ExecuteNonQuery();
            }

            return Convert.ToInt32(new MySqlCommand("SELECT LAST_INSERT_ID()", con).ExecuteScalar());
        }

        private static void EnsureStarterDrives(MySqlConnection con, int ownerId)
        {
            foreach (var drive in StarterDrives)
            {
                using (var check = new MySqlCommand("SELECT COUNT(*) FROM GameDrive WHERE drive_name=@name", con))
                {
                    check.Parameters.AddWithValue("@name", drive.Name);
                    if (Convert.ToInt32(check.ExecuteScalar()) > 0) continue;
                }

                using (var insert = new MySqlCommand(@"
                    INSERT INTO GameDrive
                        (owner_id, drive_name, description, location, duration_hours, price, max_guests, available, image_path)
                    VALUES
                        (@owner, @name, @description, @location, @hours, @price, @guests, 1, @image)", con))
                {
                    insert.Parameters.AddWithValue("@owner", ownerId);
                    insert.Parameters.AddWithValue("@name", drive.Name);
                    insert.Parameters.AddWithValue("@description", drive.Description);
                    insert.Parameters.AddWithValue("@location", drive.Location);
                    insert.Parameters.AddWithValue("@hours", drive.DurationHours);
                    insert.Parameters.AddWithValue("@price", drive.Price);
                    insert.Parameters.AddWithValue("@guests", drive.MaxGuests);
                    insert.Parameters.AddWithValue("@image", drive.ImagePath);
                    insert.ExecuteNonQuery();
                }
            }
        }

        private static void EnsureStarterAccommodations(MySqlConnection con, int ownerId)
        {
            foreach (var stay in StarterAccommodations)
            {
                using (var check = new MySqlCommand("SELECT COUNT(*) FROM Accommodation WHERE accommodation_name=@name", con))
                {
                    check.Parameters.AddWithValue("@name", stay.Name);
                    if (Convert.ToInt32(check.ExecuteScalar()) > 0) continue;
                }

                using (var insert = new MySqlCommand(@"
                    INSERT INTO Accommodation
                        (owner_id, accommodation_name, description, location, room_type, price_per_night, max_guests, available, image_path)
                    VALUES
                        (@owner, @name, @description, @location, @room, @price, @guests, 1, @image)", con))
                {
                    insert.Parameters.AddWithValue("@owner", ownerId);
                    insert.Parameters.AddWithValue("@name", stay.Name);
                    insert.Parameters.AddWithValue("@description", stay.Description);
                    insert.Parameters.AddWithValue("@location", stay.Location);
                    insert.Parameters.AddWithValue("@room", stay.RoomType);
                    insert.Parameters.AddWithValue("@price", stay.PricePerNight);
                    insert.Parameters.AddWithValue("@guests", stay.MaxGuests);
                    insert.Parameters.AddWithValue("@image", stay.ImagePath);
                    insert.ExecuteNonQuery();
                }
            }
        }

        private sealed class StarterDrive
        {
            public StarterDrive(string name, string description, string location, decimal durationHours, decimal price, int maxGuests, string imagePath)
            {
                Name = name; Description = description; Location = location;
                DurationHours = durationHours; Price = price; MaxGuests = maxGuests; ImagePath = imagePath;
            }
            public string Name { get; private set; }
            public string Description { get; private set; }
            public string Location { get; private set; }
            public decimal DurationHours { get; private set; }
            public decimal Price { get; private set; }
            public int MaxGuests { get; private set; }
            public string ImagePath { get; private set; }
        }

        private sealed class StarterAccommodation
        {
            public StarterAccommodation(string name, string description, string location, string roomType, decimal pricePerNight, int maxGuests, string imagePath)
            {
                Name = name; Description = description; Location = location;
                RoomType = roomType; PricePerNight = pricePerNight; MaxGuests = maxGuests; ImagePath = imagePath;
            }
            public string Name { get; private set; }
            public string Description { get; private set; }
            public string Location { get; private set; }
            public string RoomType { get; private set; }
            public decimal PricePerNight { get; private set; }
            public int MaxGuests { get; private set; }
            public string ImagePath { get; private set; }
        }

        private static void EnsureStarterImagePaths(MySqlConnection con)
        {
            string[,] drives = {
                {"Sunrise Wildlife Drive", "Images/Sunrise Wildlife Drive.jpg"},
                {"Golden Hour Safari", "Images/Golden Hour Safari.jpg"},
                {"Big Five Adventure", "Images/Big Five Adventure.jpg"},
                {"Bushveld Discovery Drive", "Images/Bushveld Discovery Drive.png"},
                {"Night Safari Experience", "Images/Night Safari Experience.jpg"}
            };
            for (int i=0;i<drives.GetLength(0);i++) { using(var cmd=new MySqlCommand("UPDATE GameDrive SET image_path=@img WHERE drive_name=@name AND (image_path IS NULL OR image_path='')",con)){cmd.Parameters.AddWithValue("@img",drives[i,1]);cmd.Parameters.AddWithValue("@name",drives[i,0]);cmd.ExecuteNonQuery();} }
            string[,] stays = {
                {"Impala Safari Camp", "Images/A1.jpg"}, {"Mdonya Old River Camp", "Images/A2.jpg"}, {"Golden Glamping Retreat", "Images/A3.jpg"},
                {"Ranger Luxury Tented Lodge", "Images/Luxury Tent.avif"}, {"Bushveld Romantic Chalet", "Images/Safari Chalet.webp"}, {"Wilderness Family Camp", "Images/Family Safari Tent.png"}
            };
            for (int i=0;i<stays.GetLength(0);i++) { using(var cmd=new MySqlCommand("UPDATE Accommodation SET image_path=@img WHERE accommodation_name=@name AND (image_path IS NULL OR image_path='')",con)){cmd.Parameters.AddWithValue("@img",stays[i,1]);cmd.Parameters.AddWithValue("@name",stays[i,0]);cmd.ExecuteNonQuery();} }
        }
    }
}
