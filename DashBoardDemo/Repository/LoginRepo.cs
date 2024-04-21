using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DashBoardDemo.Repository
{
    public class LoginRepo
    {
        private readonly AppDbContext appDbContext;

        public LoginRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }



        public async Task InsertTimeSlot(patientLogin timeSlot)
        {
            appDbContext.patientLogin.Add(timeSlot);
            await appDbContext.SaveChangesAsync();
        }

        public patientLogin AuthenticateUser(string username, string passcode)
        {
            // Perform your authentication logic here, e.g., querying the database for username and password
            // Return true if authentication is successful, false otherwise
            string decryptedPassword = DecryptPassword(passcode);
            var user = appDbContext.patientLogin.FirstOrDefault(u => u.UserName == username && u.passcode == decryptedPassword);
            return user;
        }

        public string GetUserRole(string username)
        {
            // Query the database to retrieve the user's role based on the username
            var user = appDbContext.patientLogin.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                return user.UserRole;
            }
            else
            {
                // Return a default role or handle the scenario where the user is not found
                return "unknown";
            }
        }
        private string DecryptPassword(string passcode)
        {
            // Implement your password encryption logic here
            // Use a suitable encryption algorithm (e.g., hashing) to encrypt the password
            // Return the encrypted password

            // Example using SHA256 encryption
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(passcode);

                // Compute the hash value of the password bytes
                byte[] hashBytes = sha256Hash.ComputeHash(passwordBytes);

                // Convert the hash bytes to a hexadecimal string
                string encryptedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return encryptedPassword;
            }
        }

        public async Task<bool> CheckDuplicacyForUsername(string username)
        {
            try
            {
                var patientUserName = await appDbContext.patientLogin.FirstOrDefaultAsync(x => x.UserName == username);


                if (patientUserName != null)
                {

                    return true; // Password update successful
                }

                return false; // Employee not found
            }
            catch (Exception ex)
            {
                return false; // Handle any exceptions that occurred during password update
            }
        }

        public string GetPatientName(int patientId)
        {


            var patient = appDbContext.Patients.FirstOrDefault(e => e.Id == patientId);

            if (patient != null)
            {
                return patient.Name;
            }

            return null;

        }

    }
}
