using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Repository;

namespace DashBoardDemo.Services
{
    public class PieChartService : IPieChartService
    {
        private readonly PieChartRepo pieChartRepo;
        

        public PieChartService(PieChartRepo pieChartRepo)
        {
            this.pieChartRepo = pieChartRepo;
           
        }
        public async Task<IEnumerable<TimeSlot_Patients>> GetTimeSlotRecords()
        {
            return await pieChartRepo.GetTimeSlotRecords();
        }
    }
}
