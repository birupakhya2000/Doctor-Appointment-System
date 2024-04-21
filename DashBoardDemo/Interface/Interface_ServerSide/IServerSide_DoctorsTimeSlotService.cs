using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;

namespace DashBoardDemo.Interface.Interface_ServerSide
{
    public interface IServerSide_DoctorsTimeSlotService
    {
        Task<DataTableResponse<View_DoctorTimeSlot>> GetDoctorsSlotTime(DataTableRequest dataTableRequest, string sortColumn, string sortColumnDirection);
        Task<List<Doctors>> GetAllDoctors();


        Task<bool> DoctorExists(int doctorId);
        Task<bool> InsertSlot(View_DoctorTimeSlot data);
        Task<bool> UpdateSlot(View_DoctorTimeSlot data);
        Task<IEnumerable<View_DoctorTimeSlot>> GetPutDocTimeSlotData();
    }
}
