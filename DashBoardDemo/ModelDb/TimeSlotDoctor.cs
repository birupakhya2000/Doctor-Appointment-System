using System.ComponentModel.DataAnnotations;

namespace DashBoardDemo.ModelDb
{
    
    public class TimeSlotDoctor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime FromTime { get; set; }
        [Required]
        public DateTime ToTime { get; set; }
    }

}
