using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.Services
{
    public class patientLoginService : IpatientLoginService
    {

        private readonly PatientsRepos patientsRepos;

        private readonly LoginRepo loginRepo;
        private readonly AppDbContext appDbContext;

        public patientLoginService(AppDbContext appDbContext, PatientsRepos patientsRepos, LoginRepo loginRepo)
        {
            this.appDbContext = appDbContext;
            this.patientsRepos = patientsRepos;
            this.loginRepo = loginRepo;

        }

        public async Task InsertPatientAndTimeSlot(Patient patientViewModel, PatientLogin patientLoginViewModel)
        {
            try
            {
                var patient = new Patients
                {
                    Name = patientViewModel.Name,
                    Dob = patientViewModel.Dob,
                    Phone = patientViewModel.Phone,
                    Email = patientViewModel.Email
                };

                var insertedPatient = await patientsRepos.InsertPatient(patient);

                var timeSlot = new patientLogin
                {
                    PatientId = insertedPatient.Id,
                    UserName = patientLoginViewModel.UserName,
                    passcode = patientLoginViewModel.passcode
                };
                timeSlot.EncryptPasscode();

                await loginRepo.InsertTimeSlot(timeSlot);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CheckDuplicacyForEmail(string email)
        {
            return await patientsRepos.CheckDuplicacyForEmail(email)
;
        }

        public async Task<bool> CheckDuplicacyForUserName(string username)
        {
            return await loginRepo.CheckDuplicacyForUsername(username);
        }


        /* public async Task<patientLogin> AuthenticatePatient(string username, string passcode)
         {
             try
             {
                 var succeeded = await appDbContext.patientLogin.FirstOrDefaultAsync(authUser =>
                     authUser.UserName == username && authUser.passcode == passcode);

                 return succeeded;
             }
             catch (Exception ex)
             {
                 // Log and handle any exceptions that occur
                 Console.WriteLine($"An error occurred during patient authentication: {ex.Message}");
                 throw;
             }
         }*/




        /*public async Task<patientLogin> AuthenticateUser(string username, string passcode)
        {
            var succeeded = await appDbContext.patientLogin.FirstOrDefaultAsync(authUser => authUser.UserName == username && authUser.passcode == passcode);
            return succeeded;
        }

        public async Task<IEnumerable<patientLogin>> getuser()
        {
            return await appDbContext.patientLogin.ToListAsync();
        }*/

        public patientLogin AuthenticateUser(string username, string passcode)
        {
            return loginRepo.AuthenticateUser(username, passcode);
        }




        public string GetUserRole(string username)
        {
            return loginRepo.GetUserRole(username);
        }


        public string GetPatientName(int patientId)
        {
            return loginRepo.GetPatientName(patientId);
        }
    }
}
