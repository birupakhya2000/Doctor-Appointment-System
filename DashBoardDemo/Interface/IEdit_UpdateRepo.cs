using DashBoardDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface IEdit_UpdateRepo
    {
        Task<IEnumerable<View_DoctorTimeSlot>> GetPutDocTimeSlotData();
    }
}