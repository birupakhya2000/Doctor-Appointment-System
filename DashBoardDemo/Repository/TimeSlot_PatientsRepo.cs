using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DashBoardDemo.Repository
{
    public class TimeSlot_PatientsRepo
    {
        private readonly AppDbContext appDbContext;

        public TimeSlot_PatientsRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        //USED FOR TO GET ALL TIMESLOT OF PATIENTS
        public async Task<List<TimeSlot_Patients>> GetAllTimeSlot()
        {
            try
            {
                var data = appDbContext.TimeSlot_Patients.ToList();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }





        //INSERT DATA FROM THE FROM INTO PATIENTS TABLE AND VIEW_PATIENTSTIMESLOT TABLE

        /*public async Task InsertTimeSlot(TimeSlot_Patients timeSlot)
        {
            try
            {
                appDbContext.TimeSlot_Patients.Add(timeSlot);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }*/

        /*public async Task<IEnumerable<TimeSlot_Patients>> InsertTimeSlot(int userId)
        {


            var data = (from pt in appDbContext.TimeSlot_Patients
                        where pt.PatientId == userId
                        select new TimeSlot_Patients
                        {
                            Id = pt.Id,
                            Date = pt.Date,
                            SlotTime = pt.SlotTime

                        }).ToList();

            return await Task.FromResult(data);
        }*/


        public async Task AddTimeSlot(int patientId, TimeSlot_Patients timeSlot_Patients)
        {
            try
            {
                var timeSlot = new TimeSlot_Patients
                {
                    PatientId = patientId,
                    DoctorId = timeSlot_Patients.DoctorId,
                    Date = timeSlot_Patients.Date,
                    SlotTime = timeSlot_Patients.SlotTime,
                    IsApproved = null
                };

                appDbContext.TimeSlot_Patients.Add(timeSlot);
                await appDbContext.SaveChangesAsync();
            }
            catch ( Exception ex )
            {

                throw;
            }
           
        }

        //TO SHOW PATIENTS TIMESLOTS IN TABLE IN ADMIN PAGE 
        public async Task<IEnumerable<View_PatientTimeSlot>> GetPatientsTimeSlot()
        {
            try
            {
                //return await appDbContext.TimeSlot_Patients
                //    .Join(appDbContext.Patients, a => a.PatientId, e => e.Id, (a, e) => new { TimeSlot = a, Patient = e })
                //    .Join(appDbContext.Doctors, ae => ae.TimeSlot.DoctorId, d => d.Id, (ae, d) => new View_PatientTimeSlot
                //    {
                //        Id = ae.TimeSlot.Id,
                //        PatientId = ae.Patient.Id,
                //        Name = ae.Patient.Name,
                //        Date = ae.TimeSlot.Date,
                //        SlotTime = ae.TimeSlot.SlotTime ?? "",
                //        IsApproved = ae.TimeSlot.IsApproved,
                //        DoctorName = d.DoctorName, // Add DoctorName from the Doctors table
                //        DoctorId = d.Id
                //    })
                //    .ToListAsync();

                var data = (from ts in appDbContext.TimeSlot_Patients
                            join p in appDbContext.Patients on ts.PatientId equals p.Id
                            join d in appDbContext.Doctors on ts.DoctorId equals d.Id
                            select new View_PatientTimeSlot
                            {
                                Id = ts.Id,
                                PatientId = p.Id,
                                Name = p.Name,
                                Date = ts.Date,
                                SlotTime = ts.SlotTime ?? string.Empty,
                                IsApproved=ts.IsApproved,
                                DoctorName=d.DoctorName,
                                DoctorId=d.Id,
                            }).OrderByDescending(x=>x.Id).ToList();

                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //WORK -> TO UPDATE ISAPPROVED COLUMN VALUE IN THE DATABASE TABLE TIMESLOT_PATIENTS AND SHOW IN TABLE APPROVED OR REJECTED
        public async Task UpdateApprovalStatus(int id, bool isApproved)
        {
            try
            {
                var timeSlotPatient = await appDbContext.TimeSlot_Patients.FindAsync(id);
                if (timeSlotPatient != null)
                {
                    timeSlotPatient.IsApproved = isApproved ? true : false;
                    await appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

       /* public async Task<IEnumerable<Patients>> FilterPatientsSlotTime(string Name)
        {
            var query = appDbContext.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(p => p.Name.Contains(Name));
            }



            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TimeSlot_Patients>> FilterPatientsSlotTime(DateTime date)
        {
            var query = appDbContext.TimeSlot_Patients.AsQueryable();

            if (!string.IsNullOrEmpty(date))
            {
                query = query.Where(p => p.date.Contains(date));
            }



            return await query.ToListAsync();
        }*/


    }
}
