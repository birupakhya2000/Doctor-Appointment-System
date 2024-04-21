using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;

namespace DashBoardDemo.Interface
{
    public interface IPatientDataService
    {
        Task<IEnumerable<Patients>> GetAttendanceWithPatientDataAsync(int userId);
        Task<IEnumerable<View_PatientTimeSlot>> GetPatientTimeslotDataAsync(int userId, string UserRole);
    }
}
