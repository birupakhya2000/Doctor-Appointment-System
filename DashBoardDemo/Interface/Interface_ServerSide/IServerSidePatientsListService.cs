using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;

namespace DashBoardDemo.Interface.Interface_ServerSide
{
    public interface IServerSidePatientsListService
    {
        IQueryable<Patients> GetPatientsList(string searchValue, string sortColumn, string sortColumnDirection);
        Task<Patients> Update(Patient emp);
        Task<List<Patients>> Delete(int Id);
        Task<Patients> GetByIdPatient(int Id);
    }
}
