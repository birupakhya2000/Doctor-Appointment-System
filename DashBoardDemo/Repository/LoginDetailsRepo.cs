using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;
using System.Security.Cryptography;
using System.Net.Mail;

namespace DashBoardDemo.Repository
{
    public class LoginDetailsRepo
    {

        private readonly AppDbContext appDbContext;

        public LoginDetailsRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<int?> VerifyEmail(string email)
        {
            var patient = await appDbContext.Patients.FirstOrDefaultAsync(x => x.Email == email);

            if (patient != null)
            {
                // Generate a random OTP
                Random random = new Random();
                int otp = random.Next(1000, 9999);

                // Update the OTP in the database for the specific employee
                patient.Otp = otp;
                await appDbContext.SaveChangesAsync();


                if (!string.IsNullOrEmpty(patient.Email))
                {
                    SendOTPMail(patient.Email, Convert.ToString(otp));
                }



                return patient.Id; 
            }

            return null; // Return a not found response
        }

        

        public async Task<int?> VerifyOTP(int userId,int otp)
        {
            var patient = await appDbContext.Patients.FirstOrDefaultAsync(x => x.Id == userId && x.Otp == otp);

            if (patient != null)
            {
                patient.Otp = null;
                await appDbContext.SaveChangesAsync();

                return patient.Id; // Return the employee ID
            }

            return null; // Return null if the OTP verification fails
        }

      
        private string EncryptPasscode(string password)
        {
            // Implement your password encryption logic here
            // Use a suitable encryption algorithm (e.g., hashing) to encrypt the password
            // Return the encrypted password

            // Example using SHA256 encryption
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password bytes
                byte[] hashBytes = sha256Hash.ComputeHash(passwordBytes);

                // Convert the hash bytes to a hexadecimal string
                string encryptedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return encryptedPassword;
            }
        }

        public async Task<bool> UpdatePassword(int userId, string newPassword)
        {
            try
            {
                var patient = await appDbContext.patientLogin.FirstOrDefaultAsync(x => x.PatientId == userId);

                if (patient != null)
                {

                    string encryptedPasscode = EncryptPasscode(newPassword);
                    // Update the password
                    patient.passcode = encryptedPasscode;
                    await appDbContext.SaveChangesAsync();

                    return true; // Password update successful
                }

                return false; // Employee not found
            }
            catch (Exception ex)
            {
                return false; // Handle any exceptions that occurred during password update
            }
        }


        //Send otp to the Email and its If block is in VerifyEmail for send Otp
        private void SendOTPMail(string EmailId, string Otp)
        {
            try
            {
                string From = "NoReply@test.com";
                string subject = "OTP for reset password.";
                string mailbody = "OTP for rester password : " + Otp;

                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(EmailId));
                mail.From = new MailAddress(From);
                mail.Subject = subject;
                mail.Body = mailbody;
                mail.IsBodyHtml = true;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("rajadas1999suman20051999@gmail.com", "rwnjthtqvvgytkzo");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
