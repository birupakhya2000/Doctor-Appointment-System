using DashBoardDemo.Models;

namespace DashBoardDemo.Repository
{
    public class StatisticsRepo
    {
        private readonly AppDbContext appDbContext;

        public StatisticsRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public int GetTotalDoctors()
        {
            return appDbContext.Doctors.Count();
        }

        public int GetTotalPatients()
        {
            return appDbContext.Patients.Count();
        }

        public int GetTodaysPatients()
        {
            DateTime today = DateTime.Today;
            return appDbContext.TimeSlot_Patients.Count(p => p.Date == today);
        }
        public int ApprovedApp()
        
        
        {
            DateTime today = DateTime.Today;
            return appDbContext.TimeSlot_Patients.Count(p => p.IsApproved == true && p.Date == today);
        }
        public int RejectApp()
        {
            DateTime today = DateTime.Today;
            return appDbContext.TimeSlot_Patients.Count(p => p.IsApproved == false && p.Date == today);
        }
    }
}
