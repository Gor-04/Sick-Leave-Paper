using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace UFAR.Classwork.UI.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Example of password hashing logic using a secure algorithm (e.g., SHA256, bcrypt, etc.)
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password cannot be empty.");

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);  // Return the hashed password as a Base64 string
            }
        }
    }
}
