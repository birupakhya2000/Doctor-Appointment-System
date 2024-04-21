using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using static DashBoardDemo.Interface.IAdminDemoService;

namespace DashBoardDemo.Services
{
    public class AdminDemoService : IAdminDemoService
    {
        private readonly PatientsRepo patientsRepo;
        private readonly DoctorsRepo doctorsRepo;
        private readonly TimeSlot_PatientsRepo timeSlot_PatientsRepo;
        private readonly Edit_UpdateRepo edit_UpdateRepo;
        private readonly AppDbContext appDbContext;

        public AdminDemoService(AppDbContext appDbContext,PatientsRepo patientsRepo,DoctorsRepo doctorsRepo, TimeSlot_PatientsRepo timeSlot_PatientsRepo, Edit_UpdateRepo edit_UpdateRepo)
        {
            this.appDbContext = appDbContext;
            this.patientsRepo = patientsRepo;
            this.doctorsRepo = doctorsRepo;
            this.timeSlot_PatientsRepo = timeSlot_PatientsRepo;
            this.edit_UpdateRepo = edit_UpdateRepo;
        }
       
       



        //TO SHOW IN TABLE PATINETS DATA
        public async Task<List<Patients>> GetAllData()
        {
            try
            {
                var data = await patientsRepo.GetAllData();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //FOR UPDATE OF PATIENTS TABLE
        public async Task<Patients> Update(Patient emp)
        {
            try
            {
                var data = await patientsRepo.Update(emp);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //For edit Patients data 
        public async Task<Patients> GetByIdPatient(int Id)
        {
            try
            {
                var data = await patientsRepo.GetByIdPatient(Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //For Searching
        public async Task<IEnumerable<Patients>> FilterDataSearching( string PatientName)
        {
            return await patientsRepo.FilterDataSearching( PatientName);
        }
       

        public async Task<List<Patients>> Delete(int Id)
        {
            try
            {
                var data = await patientsRepo.Delete(Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //USED TI SHOW ALL DOCTORS DATA
        public async Task<List<Doctors>> GetAllDataDoctor()
        {
            try
            {
                var data = await doctorsRepo.GetAllDataDoctor();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //INSERT AND UPDATE IN DOCTORS IN THE DOCTOR MASTER
        public async Task<Doctors> Insert(Doctor emp)
        {
            try
            {
                var data = await doctorsRepo.Insert(emp);
                return await Task.FromResult(data);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Doctors> Update(Doctor emp)
        {
            try
            {
                var data = await doctorsRepo.Update(emp);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //For edit DoctorMaster
        public async Task<Doctors> GetById(int Id)
        {
            try
            {
                var data = await doctorsRepo.GetById(Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        //USED TO SHOW ALL TIMESLOT OF PATIENTS DATA
        public async Task<List<TimeSlot_Patients>> GetAllTimeSlot()
        {
            try
            {
                var data = await timeSlot_PatientsRepo.GetAllTimeSlot();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        
        //Used to show all doctor names in a dropdown in AddDoc view
        public async Task<List<Doctors>> GetAllDoctors()
        {
            try
            {
                var doctors = await doctorsRepo.GetAllDoctors();
                return doctors;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //End



         //For Insert data into  TimeSlotDoctor  table(Below 2 are for insert) Start
        public async Task<bool> DoctorExists(int doctorId)
        {
            var doctor = await doctorsRepo.GetByIdDoc(doctorId);
            return (doctor != null);
        }

        public async Task<bool> InsertSlot(View_DoctorTimeSlot data)
        {
            try
            {
                await doctorsRepo.InsertSlot(data);
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }
        //For Update
        public async Task<bool> UpdateSlot(View_DoctorTimeSlot data)
        {
            try
            {
                await doctorsRepo.UpdateSlot(data);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //End





        //To show TimeSlotDocor Data in Table
        public async Task<IEnumerable<View_DoctorTimeSlot>> GetDoctorsTimeSlot()
        {
            try
            {
                return await doctorsRepo.GetDoctorsTimeSlot();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public async Task<IEnumerable<View_DoctorTimeSlot>> GetPutDocTimeSlotData()
        {
            try
            {
                return await edit_UpdateRepo.GetPutDocTimeSlotData();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        

        /*  public async Task<IEnumerable<View_DoctorTimeSlot>> EditDocSlot(int Id, int DoctorId)
          {
              try
              {
                  return await doctorsRepo.EditDocSlot(Id, DoctorId);
              }
              catch (Exception ex)
              {

                  throw;
              }

          }*/
        //TO SHOW PATIENTS TIMESLOTS IN TABLE 
        public async Task<IEnumerable<View_PatientTimeSlot>> GetPatientsTimeSlot()
        {
            try
            {
                return await timeSlot_PatientsRepo.GetPatientsTimeSlot();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw;
            }
        }

        //WORK -> TO UPDATE ISAPPROVED COLUMN VALUE IN THE DATABASE TABLE TIMESLOT_PATIENTS AND SHOW IN TABLE APPROVED OR REJECTED

        public async Task UpdateApprovalStatus(int id, bool isApproved)
        {
            try
            {
                await timeSlot_PatientsRepo.UpdateApprovalStatus(id, isApproved);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


        

    }
}
