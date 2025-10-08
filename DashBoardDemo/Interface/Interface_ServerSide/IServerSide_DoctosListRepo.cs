using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using System.Linq;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface.Interface_ServerSide
{
    public interface IServerSide_DoctosListRepo
    {
        IQueryable<Doctors> GetDoctorList(string searchValue, string sortColumn, string sortColumnDirection);
        Task<Doctors> GetById(int Id);
        Task<Doctors> Insert(Doctor add);
        Task<Doctors> Update(Doctor add);
        Task<DataTableResponse<Doctors>> FilterDataSearching(string doctorName, DataTableRequest request, string searchValue, string sortColumn, string sortColumnDirection);
    }
}