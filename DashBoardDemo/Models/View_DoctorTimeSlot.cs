namespace DashBoardDemo.Models
{
    public class View_DoctorTimeSlot
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public DateTime SlotTime { get; set; }
    }
}
