using System;
using System.Collections.Generic;
using System.IO;

namespace TheRanger
{
    public static class ImageHelper
    {
        public static string DefaultAccommodationImage { get { return "Images/A1.jpg"; } }
        public static string DefaultGameDriveImage { get { return "Images/D1.jpg"; } }

        public static string AccommodationImage(string name, string imagePath = null)
        {
            if (IsSafeExistingImage(imagePath)) return imagePath.Replace("\\", "/");
            string value = (name ?? string.Empty).ToLowerInvariant();
            if (value.Contains("impala") || value.Contains("river")) return value.Contains("river") ? "Images/57-Mdonya-Old-River-Camp.webp" : "Images/A1.jpg";
            if (value.Contains("family")) return "Images/Family Safari Tent.png";
            if (value.Contains("glamp")) return "Images/Glamping Tent.avif";
            if (value.Contains("chalet")) return "Images/Safari Chalet.webp";
            if (value.Contains("luxury") || value.Contains("tent")) return "Images/Luxury Tent.avif";
            return DefaultAccommodationImage;
        }

        public static string GameDriveImage(string name, string imagePath = null)
        {
            if (IsSafeExistingImage(imagePath)) return imagePath.Replace("\\", "/");
            string value = (name ?? string.Empty).ToLowerInvariant();
            if (value.Contains("sunrise")) return "Images/Sunrise Wildlife Drive.jpg";
            if (value.Contains("golden")) return "Images/Golden Hour Safari.jpg";
            if (value.Contains("big five")) return "Images/Big Five Adventure.jpg";
            if (value.Contains("bushveld")) return "Images/Bushveld Discovery Drive.png";
            if (value.Contains("night")) return "Images/Night Safari Experience.jpg";
            return DefaultGameDriveImage;
        }

        public static List<string> ExistingImages(string relativeDirectory, bool gameDrive)
        {
            var result = new List<string>();
            string physical = System.Web.Hosting.HostingEnvironment.MapPath("~/" + relativeDirectory.TrimStart('~','/'));
            if (!Directory.Exists(physical)) return result;
            foreach (string file in Directory.GetFiles(physical))
            {
                string ext = Path.GetExtension(file).ToLowerInvariant();
                if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".webp" && ext != ".avif") continue;
                string name = Path.GetFileName(file);
                if (gameDrive)
                {
                    bool match = name.StartsWith("D", StringComparison.OrdinalIgnoreCase) || name.StartsWith("game-drive-", StringComparison.OrdinalIgnoreCase) ||
                                 name.Equals("Big Five Adventure.jpg", StringComparison.OrdinalIgnoreCase) || name.Equals("Bushveld Discovery Drive.png", StringComparison.OrdinalIgnoreCase) ||
                                 name.Equals("Golden Hour Safari.jpg", StringComparison.OrdinalIgnoreCase) || name.Equals("Night Safari Experience.jpg", StringComparison.OrdinalIgnoreCase) ||
                                 name.Equals("Sunrise Wildlife Drive.jpg", StringComparison.OrdinalIgnoreCase);
                    if (match) result.Add("Images/" + name);
                }
                else
                {
                    bool match = name.StartsWith("A", StringComparison.OrdinalIgnoreCase) || name.StartsWith("accommodation-", StringComparison.OrdinalIgnoreCase) ||
                                 name.Equals("37-Selous-Impala-Camp.webp", StringComparison.OrdinalIgnoreCase) || name.Equals("57-Mdonya-Old-River-Camp.webp", StringComparison.OrdinalIgnoreCase) ||
                                 name.Equals("Family Safari Tent.png", StringComparison.OrdinalIgnoreCase) || name.Equals("Glamping Tent.avif", StringComparison.OrdinalIgnoreCase) ||
                                 name.Equals("Luxury Tent.avif", StringComparison.OrdinalIgnoreCase) || name.Equals("River Tent.jpg", StringComparison.OrdinalIgnoreCase) ||
                                 name.Equals("Safari Chalet.webp", StringComparison.OrdinalIgnoreCase) || name.Equals("Handcrafted-Luxury-Safari-tents-Luxury-Tent-Manufacturer.jpg", StringComparison.OrdinalIgnoreCase) ||
                                 name.Equals("romantic-tented-chalet-south-africa7.jpg", StringComparison.OrdinalIgnoreCase) || name.Equals("luxury-glamping-tent-safari-sunset-stock-photo-scaled.webp", StringComparison.OrdinalIgnoreCase);
                    if (match) result.Add("Images/" + name);
                }
            }
            result.Sort(StringComparer.OrdinalIgnoreCase);
            return result;
        }

        private static bool IsSafeExistingImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) return false;
            string normalized = imagePath.Replace("\\", "/").Trim();
            if (!normalized.StartsWith("Images/", StringComparison.OrdinalIgnoreCase)) return false;
            string physical = System.Web.Hosting.HostingEnvironment.MapPath("~/" + normalized);
            return !string.IsNullOrWhiteSpace(physical) && File.Exists(physical);
        }
    }
}
