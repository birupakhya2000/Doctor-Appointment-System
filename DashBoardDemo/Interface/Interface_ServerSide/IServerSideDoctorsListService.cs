using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;

namespace DashBoardDemo.Interface.Interface_ServerSide
{
    public interface IServerSideDoctorsListService
    {
        IQueryable<Doctors> GetDoctorList(string searchValue, string sortColumn, string sortColumnDirection);
        Task<Doctors> GetById(int Id);
        Task<Doctors> Insert(Doctor emp);
        Task<Doctors> Update(Doctor emp);
        Task<DataTableResponse<Doctors>> FilterDataSearching(string doctorName, DataTableRequest request, string searchValue, string sortColumn, string sortColumnDirection);
    }
}
