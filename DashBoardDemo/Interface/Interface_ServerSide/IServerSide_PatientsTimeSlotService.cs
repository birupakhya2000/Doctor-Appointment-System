using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;

namespace DashBoardDemo.Interface.Interface_ServerSide
{
    public interface IServerSide_PatientsTimeSlotService
    {
        Task<DataTableResponse<View_PatientTimeSlot>> GetPatientsSlotTime(DataTableRequest dataTableRequest, string sortColumn, string sortColumnDirection);
    }
}
