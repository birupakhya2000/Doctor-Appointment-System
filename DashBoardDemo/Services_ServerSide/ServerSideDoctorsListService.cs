using DashBoardDemo.Interface.Interface_ServerSide;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using DashBoardDemo.Repository;
using DashBoardDemo.ServerSideRepository;
using NuGet.Protocol.Core.Types;

namespace DashBoardDemo.Services_ServerSide
{
    public class ServerSideDoctorsListService : IServerSideDoctorsListService
    {
        private readonly ServerSide_DoctosListRepo serverSide_DoctosListRepo;
       
        public ServerSideDoctorsListService(ServerSide_DoctosListRepo serverSide_DoctosListRepo)
        {
            this.serverSide_DoctosListRepo = serverSide_DoctosListRepo;
           
        }

        public IQueryable<Doctors> GetDoctorList(string searchValue, string sortColumn, string sortColumnDirection)
        {
            
            return serverSide_DoctosListRepo.GetDoctorList(searchValue,sortColumn,sortColumnDirection);
        }

        public async Task<Doctors> GetById(int Id)
        {
            try
            {
                var data = await serverSide_DoctosListRepo.GetById(Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        



        public async Task<Doctors> Insert(Doctor emp)
        {
            try
            {
                var data = await serverSide_DoctosListRepo.Insert(emp);
                return await Task.FromResult(data);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Doctors> Update(Doctor emp)
        {
            try
            {
                var data = await serverSide_DoctosListRepo.Update(emp);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //For Searching
        public Task<DataTableResponse<Doctors>> FilterDataSearching(string doctorName, DataTableRequest request, string searchValue, string sortColumn, string sortColumnDirection)
        {
            return serverSide_DoctosListRepo.FilterDataSearching(doctorName, request, searchValue, sortColumn, sortColumnDirection);
        }
    }
}
