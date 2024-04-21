using DashBoardDemo.ModelDb;

namespace DashBoardDemo.Interface
{
    public interface IPieChartService 
    {
        Task<IEnumerable<TimeSlot_Patients>> GetTimeSlotRecords();
    }
}
