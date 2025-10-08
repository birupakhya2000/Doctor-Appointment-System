using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface IPatientDataRepo
    {
        Task<IEnumerable<Patients>> GetAttendanceWithPatientDataAsync(int userId);
        Task<IEnumerable<View_PatientTimeSlot>> GetPatientTimeslotDataAsync(int userId, string UserRole);
    }
}