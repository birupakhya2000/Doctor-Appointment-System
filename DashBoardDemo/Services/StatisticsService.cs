using DashBoardDemo.Interface;
using DashBoardDemo.Repository;

namespace DashBoardDemo.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly StatisticsRepo statisticsRepo;

        public StatisticsService(StatisticsRepo statisticsRepo)
        {
            this.statisticsRepo = statisticsRepo;
        }

        public int GetTotalDoctors()
        {
            return statisticsRepo.GetTotalDoctors();
        }

        public int GetTotalPatients()
        {
            return statisticsRepo.GetTotalPatients();
        }

        public int GetTodaysPatients()
        {
            return statisticsRepo.GetTodaysPatients();
        }
        public int ApprovedApp()
        {
            return statisticsRepo.ApprovedApp();
        }
        public int RejectApp()
        {
            return statisticsRepo.RejectApp();
        }
    }
}
