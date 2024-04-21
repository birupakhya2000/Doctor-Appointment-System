using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;

namespace DashBoardDemo.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly DoctorsRepos doctorsRepos;

        public DoctorsService(DoctorsRepos doctorsRepos)
        {
            this.doctorsRepos = doctorsRepos;
        }

        public async Task<List<Doctors>> GetAllDataDoctor()
        {
            try
            {
                var data = await doctorsRepos.GetAllDataDoctor();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<Doctors> GetById(int Id)
        {
            try
            {
                var data = await doctorsRepos.GetById(Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

      
        //INSERT SLOT TIME OF DOCTORS ACCORDING TO THEIR ID IN THE PATIENTVIEW FORM
        public List<string> GetDoctorTimeSlots(int DoctorId, DateTime selectedDate)
        {
            try
            {
                var timeSlots = doctorsRepos.GetDoctorTimeSlots(DoctorId,selectedDate);
                return timeSlots;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
