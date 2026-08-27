using System;
using System.Security.Cryptography;

namespace TheRanger
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string stored)
        {
            if (string.IsNullOrWhiteSpace(stored)) return false;
            // Accept plain text too, so manually inserted university test accounts can be used.
            if (!stored.Contains(":")) return string.Equals(password, stored, StringComparison.Ordinal);
            string[] parts = stored.Split(':');
            try
            {
                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] expected = Convert.FromBase64String(parts[1]);
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
                {
                    byte[] actual = pbkdf2.GetBytes(expected.Length);
                    if (actual.Length != expected.Length) return false;
                    int result = 0;
                    for (int i = 0; i < actual.Length; i++) result |= actual[i] ^ expected[i];
                    return result == 0;
                }
            }
            catch { return false; }
        }
    }
}
