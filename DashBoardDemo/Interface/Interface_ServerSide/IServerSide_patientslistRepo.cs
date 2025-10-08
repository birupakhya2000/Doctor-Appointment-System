using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface.Interface_ServerSide
{
    public interface IServerSide_patientslistRepo
    {
        IQueryable<Patients> GetPatientsList(string searchValue, string sortColumn, string sortColumnDirection);
        Task<Patients> Update(Patient add);
        Task<Patients> GetByIdPatient(int Id);
        Task<List<Patients>> Delete(int Id);
    }
}