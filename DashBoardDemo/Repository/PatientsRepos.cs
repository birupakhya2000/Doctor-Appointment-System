using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DashBoardDemo.Repository
{
    public class PatientsRepos
    {
        private readonly AppDbContext appDbContext;

        public PatientsRepos(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Patients> GetByIdFor(int Id)
        {
            try
            {
                
                var data = appDbContext.Patients.Single(x => x.Id == Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Patients> GetPatientById(int userId)
        {
            try
            {

                var data = appDbContext.Patients.SingleOrDefault(x => x.Id == userId);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        //INSERT DATA FROM THE FROM INTO PATIENTS TABLE AND VIEW_PATIENTSTIMESLOT TABLE
      
        //<Common>
        public async Task<Patients> InsertPatient(Patients patient)
        {
            appDbContext.Patients.Add(patient);
            await appDbContext.SaveChangesAsync();
            return patient;
        }


        public async Task<IEnumerable<Patients>> PutPatientvalue(int Id)
        {


            var data = (from pt in appDbContext.Patients
                        where pt.Id == Id
                        select new Patients
                        {
                            Id = pt.Id,
                            Name = pt.Name,
                            Dob = pt.Dob,
                            Phone = pt.Phone,
                            Email = pt.Email
                        }).ToList();

            return await Task.FromResult(data);
        }

        public async Task<bool> CheckDuplicacyForEmail(string email)
        {
            try
            {
                var patientEmail = await appDbContext.Patients.FirstOrDefaultAsync(x => x.Email == email);


                if (patientEmail != null)
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




    }
}
