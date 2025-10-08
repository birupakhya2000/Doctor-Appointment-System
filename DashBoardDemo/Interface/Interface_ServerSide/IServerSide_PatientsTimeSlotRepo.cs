using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface.Interface_ServerSide
{
    public interface IServerSide_PatientsTimeSlotRepo
    {
        Task<DataTableResponse<View_PatientTimeSlot>> GetPatientsSlotTime(DataTableRequest dataTableRequest, string sortColumn, string sortColumnDirection);
    }
}