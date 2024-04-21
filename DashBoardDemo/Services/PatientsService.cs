using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly PatientsRepos patientsRepos;
        private readonly TimeSlot_PatientsRepo timeSlot_PatientsRepo;

        public PatientsService(PatientsRepos patientsRepos, TimeSlot_PatientsRepo timeSlot_PatientsRepo)
        {
            this.patientsRepos = patientsRepos;
            this.timeSlot_PatientsRepo = timeSlot_PatientsRepo;
        }

        public async Task<Patients> GetByIdFor(int Id)
        {
            try
            {
                var data = await patientsRepos.GetByIdFor(Id);
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
                var data = await patientsRepos.GetPatientById(userId);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       

        public async Task<IEnumerable<Patients>> PutPatientvalue(int Id)
        {
            var attendanceData = await patientsRepos.PutPatientvalue(Id);

            // Filter the attendance data based on the userId and userType
            return attendanceData;
        }





        //Try
        /* public async Task InsertPatientAndTimeSlot(Patient patientViewModel, View_PatientTimeSlot timeSlotViewModel)
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

                 var timeSlot = new TimeSlot_Patients
                 {
                     PatientId = insertedPatient.Id,
                    *//* DoctorId = insertedPatient.Id,*//*
                     Date = timeSlotViewModel.Date,
                     SlotTime = timeSlotViewModel.SlotTime

                 };

                 await timeSlot_PatientsRepo.InsertTimeSlot(timeSlot);


             }
             catch (Exception ex)
             {
                 throw;
             }
         }*/


        public async Task AddTimeSlot(int patientId, TimeSlot_Patients timeSlot_Patients)
        {
            await timeSlot_PatientsRepo.AddTimeSlot(patientId, timeSlot_Patients);
        }




    }
}
