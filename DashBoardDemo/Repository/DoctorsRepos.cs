using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.Repository
{
    public class DoctorsRepos
    {
        private readonly AppDbContext appDbContext;

        public DoctorsRepos(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        //THIS IS FOR THE DOCTOR CONTROLLER GET DOCTORS DATA 
        public async Task<List<Doctors>> GetAllDataDoctor()
        {
            try
            {
                var data = appDbContext.Doctors.ToList();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        //GET BY ID
        public async Task<Doctors> GetById(int Id)
        {
            try
            {

                var data = appDbContext.Doctors.Single(x => x.Id == Id);
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
                var timeSlots = appDbContext.TimeSlotDoctor
                    .Where( d => d.FromTime.Date == selectedDate.Date && d.DoctorId == DoctorId)
                    .Select(d => $"{d.FromTime.ToString("h.mmtt")} - {d.ToTime.ToString("h.mmtt")}")
                    .ToList();

                return timeSlots;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /*public async Task<IEnumerable<ViewAtnd_emp>> GetAttendanceWithEmployeeDataAsync(int userId)
        {

            DateTime currentDate = DateTime.Now.Date;
            var data = (from et in appDbContext.Emp_Attendance
                        join em in appDbContext.Employee_Master on et.Employee_id equals em.id
                        where em.id == userId && et.date == currentDate
                        select new ViewAtnd_emp
                        {
                            EmpId = em.id,
                            id = et.id,
                            name = em.name,
                            date = et.date,
                            IsPresent = et.IsPresent
                        }).ToList();

            return await Task.FromResult(data);
        }*/

       
    }


}

