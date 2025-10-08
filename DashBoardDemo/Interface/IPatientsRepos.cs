using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface IPatientsRepos
    {
        Task<Patients> GetByIdFor(int Id);
        Task<Patients> GetPatientById(int userId);
        Task<Patients> InsertPatient(Patients patient);
        Task<IEnumerable<Patients>> PutPatientvalue(int Id);
        Task<bool> CheckDuplicacyForEmail(string email);
    }
}