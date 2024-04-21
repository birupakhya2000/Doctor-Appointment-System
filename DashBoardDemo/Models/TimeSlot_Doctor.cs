using MessagePack;
using Microsoft.Build.Framework;

namespace DashBoardDemo.Models
{

    public class TimeSlot_Doctor
    {

        
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime FromTime { get; set; }
        [Required]
        public DateTime ToTime { get; set; }
    }
   
}
