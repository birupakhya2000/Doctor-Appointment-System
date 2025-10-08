using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface ITimeSlot_PatientsRepo
    {
        Task<List<TimeSlot_Patients>> GetAllTimeSlot();
        Task AddTimeSlot(int patientId, TimeSlot_Patients timeSlot_Patients);
        Task<IEnumerable<View_PatientTimeSlot>> GetPatientsTimeSlot();
        Task UpdateApprovalStatus(int id, bool isApproved);
    }
}