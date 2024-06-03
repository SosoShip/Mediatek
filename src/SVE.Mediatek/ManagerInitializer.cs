using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.Dal;
using SVE.Mediatek.DAL.Entities;
using SVE.Mediatek.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SVE.Mediatek
{
    public class ManagerInitializer
    {
        private readonly MediatekContext Context;

        public ManagerInitializer(MediatekContext context) 
        {
            Context = context;
        }

        /// <summary>
        /// Record a manager if he does not exist in the database
        /// </summary>
        public void InitializeManager()
        {
            // Check of the existence of a manager in the database.
            var managerExists = Context.Manager.Any(m => m.Email == "jcorazon@mediatek.fr");
            if (managerExists) return;

            // Record the manager if he does not exist
            // Password security in app with Secret Manager
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<ManagerInitializer>();
            var configuration = builder.Build();
            var password = configuration["ManagerPassword"];
            
            // Password security with hash and salt
            string salt = GenerateSalt(); 
            string hashedPassword = new PasswordHasher().HashPassword(password, salt);

            var manager = new ManagerEntity
            {
                Name = "Corazon",
                FirsName = "Julia",
                Email = "jcorazon@mediatek.fr",
                Phone = "0265798413",
                Department = Department.Manager,
                Password = hashedPassword,
                Salt = salt
            };

            Context.Add(manager);
            Context.SaveChanges();
        }

        /// <summary>
        /// Generates a cryptographically strong random salt.
        /// </summary>
        /// <returns>A Base64 string representation of a 32-byte salt.</returns>
        public string GenerateSalt()
        {
            byte[] salt = new byte[32]; // Generate a 32-byte salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt); // Fill the salt array with random bytes
            }
            return Convert.ToBase64String(salt); // Convert the salt into a string for storage
        }
    }
}
