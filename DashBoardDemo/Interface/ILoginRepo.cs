using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface ILoginRepo
    {
        Task InsertTimeSlot(patientLogin timeSlot);
        patientLogin AuthenticateUser(string username, string passcode);
        string GetUserRole(string username);
        Task<bool> CheckDuplicacyForUsername(string username);
        string GetPatientName(int patientId);
    }
}