using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DashBoardDemo.Repository
{
    public class PatientDataRepo
    {

        private readonly AppDbContext appDbContext;

        public PatientDataRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            
        }

        public async Task<IEnumerable<Patients>> GetAttendanceWithPatientDataAsync(int userId)
        {


            var data = (from pt in appDbContext.Patients
                        where pt.Id == userId
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


        public async Task<IEnumerable<View_PatientTimeSlot>> GetPatientTimeslotDataAsync(int userId,string UserRole)
        {
            var data = await appDbContext.TimeSlot_Patients
                .Join(appDbContext.Patients, a => a.PatientId, e => e.Id, (a, e) => new { TimeSlot = a, Patient = e })
                .Join(appDbContext.Doctors, ae => ae.TimeSlot.DoctorId, d => d.Id, (ae, d) => new View_PatientTimeSlot
                {
                    Id = ae.TimeSlot.Id,
                    PatientId = ae.Patient.Id,
                    Name = ae.Patient.Name,
                    Date = ae.TimeSlot.Date,
                    SlotTime = ae.TimeSlot.SlotTime,
                    IsApproved = ae.TimeSlot.IsApproved,
                    DoctorName = d.DoctorName, // Add DoctorName from the Doctors table
                    DoctorId = d.Id,
                    UserRole= UserRole
                })
                .Where(ae => ae.PatientId == userId) // Filter data based on the current user's ID
                .ToListAsync();

            return data;
        }


    }
}
