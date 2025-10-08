using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface IPatientsRepo
    {
        Task<List<Patients>> GetAllData();
        Task<IEnumerable<Patients>> FilterDataSearching(string PatientName);
        Task<List<Patients>> Delete(int Id);
        Task<Patients> Update(Patient add);
        Task<Patients> GetByIdPatient(int Id);
    }
}