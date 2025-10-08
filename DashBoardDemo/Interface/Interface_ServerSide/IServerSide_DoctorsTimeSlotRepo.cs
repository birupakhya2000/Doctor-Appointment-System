using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface.Interface_ServerSide
{
    public interface IServerSide_DoctorsTimeSlotRepo
    {
        Task<DataTableResponse<View_DoctorTimeSlot>> GetDoctorsSlotTime(DataTableRequest dataTableRequest, string sortColumn, string sortColumnDirection);
        Task<List<Doctors>> GetAllDoctors();
        Task<Doctors> GetByIdDoc(int id);
        Task InsertSlot(View_DoctorTimeSlot data);
        Task UpdateSlot(View_DoctorTimeSlot data);
        Task<IEnumerable<View_DoctorTimeSlot>> GetPutDocTimeSlotData();
    }
}