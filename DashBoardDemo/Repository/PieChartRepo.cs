using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.Repository
{
    public class PieChartRepo
    {
        private readonly AppDbContext appDbContext;

        public PieChartRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<TimeSlot_Patients>> GetTimeSlotRecords()
        {
            
            var records = await appDbContext.TimeSlot_Patients.ToListAsync();
            return records;
        }
    }
}
