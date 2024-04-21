using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;

namespace DashBoardDemo.Interface
{
    public interface IpatientLoginService
    {
        Task InsertPatientAndTimeSlot(Patient patientViewModel, PatientLogin timeSlotViewModel);
        patientLogin AuthenticateUser(string username, string passcode);

        string GetUserRole(string username);
        
        Task<bool> CheckDuplicacyForEmail(string email);
        Task<bool> CheckDuplicacyForUserName(string username);
        string GetPatientName(int patientId);
    }
}
