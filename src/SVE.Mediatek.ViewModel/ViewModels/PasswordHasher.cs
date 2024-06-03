using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SVE.Mediatek.ViewModel.ViewModels
{
    /// <summary>
    /// The PasswordHasher class provides functionality for hashing passwords.
    /// </summary>
    public class PasswordHasher
    {
        /// <summary>
        /// Hashes a password using SHA256.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <param name="salt">The salt to use in the hashing process.</param>
        /// <returns>The hashed password as a hexadecimal string.</returns>
        public string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create(); // New instance of the SHA256 hashing algorithm."
            var saltedPassword = password + salt;
            byte[] textData = Encoding.UTF8.GetBytes(saltedPassword); //Converts the salted password into a byte array.
            byte[] hash = sha256.ComputeHash(textData); //Calculates the SHA256 hash of the password.
            return BitConverter.ToString(hash).Replace("-", string.Empty); //Converts the byte array into a hexadecimal string.
        }
    }
}
