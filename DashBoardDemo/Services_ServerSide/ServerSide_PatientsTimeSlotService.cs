using DashBoardDemo.Interface.Interface_ServerSide;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using DashBoardDemo.ServerSideRepository;

namespace DashBoardDemo.Services_ServerSide
{
    public class ServerSide_PatientsTimeSlotService : IServerSide_PatientsTimeSlotService
    {
        private readonly ServerSide_PatientsTimeSlotRepo serverSide_PatientsTimeSlotRepo;

        public ServerSide_PatientsTimeSlotService(ServerSide_PatientsTimeSlotRepo serverSide_PatientsTimeSlotRepo)
        {
            this.serverSide_PatientsTimeSlotRepo = serverSide_PatientsTimeSlotRepo;

        }
        public Task<DataTableResponse<View_PatientTimeSlot>> GetPatientsSlotTime(DataTableRequest dataTableRequest, string sortColumn, string sortColumnDirection)
        {
            return serverSide_PatientsTimeSlotRepo.GetPatientsSlotTime(dataTableRequest, sortColumn, sortColumnDirection);
        }
    }
}
