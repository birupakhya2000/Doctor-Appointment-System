using System.ComponentModel.DataAnnotations;

namespace DashBoardDemo.ModelDb
{
    public class TimeSlot_Patients
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime Date { get; set; }
        [Required]
        public string SlotTime { get; set; }
        public bool? IsApproved { get; set; }
    }
}
