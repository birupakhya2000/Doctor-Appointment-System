namespace DashBoardDemo.Models
{
    public class View_PatientTimeSlot
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string DoctorName { get; set; }
        public DateTime Date { get; set; }
        public string SlotTime { get; set; }
        public bool? IsApproved { get; set; }
        public string? UserRole { get; set; }

    }
}
