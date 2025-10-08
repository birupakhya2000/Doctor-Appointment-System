using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface IPieChartRepo
    {
        Task<IEnumerable<TimeSlot_Patients>> GetTimeSlotRecords();
    }
}