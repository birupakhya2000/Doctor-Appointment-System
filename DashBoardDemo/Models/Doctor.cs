using System.ComponentModel.DataAnnotations;

namespace DashBoardDemo.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        
        public string DoctorName { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Speciality { get; set; }
       
    }
}
