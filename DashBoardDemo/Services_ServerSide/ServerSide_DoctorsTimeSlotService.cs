using DashBoardDemo.Interface.Interface_ServerSide;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using DashBoardDemo.Repository;
using DashBoardDemo.ServerSideRepository;

namespace DashBoardDemo.Services_ServerSide
{
    public class ServerSide_DoctorsTimeSlotService : IServerSide_DoctorsTimeSlotService
    {
        private readonly ServerSide_DoctorsTimeSlotRepo serverSide_DoctorsTimeSlotRepo;

        public ServerSide_DoctorsTimeSlotService(ServerSide_DoctorsTimeSlotRepo serverSide_DoctorsTimeSlotRepo)
        {
            this.serverSide_DoctorsTimeSlotRepo = serverSide_DoctorsTimeSlotRepo;

        }

        public Task<DataTableResponse<View_DoctorTimeSlot>> GetDoctorsSlotTime(DataTableRequest dataTableRequest, string sortColumn, string sortColumnDirection)
        {
            return serverSide_DoctorsTimeSlotRepo.GetDoctorsSlotTime(dataTableRequest, sortColumn, sortColumnDirection);
        }
        //Used to show all doctor names in a dropdown in AddDoc view
        public async Task<List<Doctors>> GetAllDoctors()
        {
            try
            {
                var doctors = await serverSide_DoctorsTimeSlotRepo.GetAllDoctors();
                return doctors;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //End



        //For Insert data into  TimeSlotDoctor  table(Below 2 are for insert) Start
        public async Task<bool> DoctorExists(int doctorId)
        {
            var doctor = await serverSide_DoctorsTimeSlotRepo.GetByIdDoc(doctorId);
            return (doctor != null);
        }

        public async Task<bool> InsertSlot(View_DoctorTimeSlot data)
        {
            try
            {
                await serverSide_DoctorsTimeSlotRepo.InsertSlot(data);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> UpdateSlot(View_DoctorTimeSlot data)
        {
            try
            {
                await serverSide_DoctorsTimeSlotRepo.UpdateSlot(data);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //End

        public async Task<IEnumerable<View_DoctorTimeSlot>> GetPutDocTimeSlotData()
        {
            try
            {
                return await serverSide_DoctorsTimeSlotRepo.GetPutDocTimeSlotData();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
