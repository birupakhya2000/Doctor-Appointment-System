using DashBoardDemo.Interface.Interface_ServerSide;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;
using DashBoardDemo.ServerSideRepository;

namespace DashBoardDemo.Services_ServerSide
{
    public class ServerSidePatientsListService : IServerSidePatientsListService
    {

        private readonly ServerSide_patientslistRepo serverSide_PatientslistRepo;

        public ServerSidePatientsListService(ServerSide_patientslistRepo serverSide_PatientslistRepo)
        {
            this.serverSide_PatientslistRepo = serverSide_PatientslistRepo;

        }

        public IQueryable<Patients> GetPatientsList(string searchValue, string sortColumn, string sortColumnDirection)
        {

            return serverSide_PatientslistRepo.GetPatientsList(searchValue, sortColumn, sortColumnDirection);
        }
        public async Task<List<Patients>> Delete(int Id)
        {
            try
            {
                var data = await serverSide_PatientslistRepo.Delete(Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Patients> Update(Patient emp)
        {
            try
            {
                var data = await serverSide_PatientslistRepo.Update(emp);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //For edit Patients data 
        public async Task<Patients> GetByIdPatient(int Id)
        {
            try
            {
                var data = await serverSide_PatientslistRepo.GetByIdPatient(Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
