using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;

namespace DashBoardDemo.Services
{
    public class PatientDataService : IPatientDataService
    {
        private readonly PatientDataRepo patientDataRepo;
        public PatientDataService( PatientDataRepo patientDataRepo) 
        {
            this.patientDataRepo = patientDataRepo;
           
        }

        public async Task<IEnumerable<Patients>> GetAttendanceWithPatientDataAsync(int userId)
        {
            var attendanceData = await patientDataRepo.GetAttendanceWithPatientDataAsync(userId);

            // Filter the attendance data based on the userId and userType
            return attendanceData;
        }

        public async Task<IEnumerable<View_PatientTimeSlot>> GetPatientTimeslotDataAsync(int userId,string UserRole)
        {
            var attendanceData = await patientDataRepo.GetPatientTimeslotDataAsync(userId, UserRole);

            // Filter the attendance data based on the userId and userType
            return attendanceData;
        }
    }
}
