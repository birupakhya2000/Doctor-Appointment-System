using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DashBoardDemo.Models
{
    public class TimeSlotPatient
    {
            [Key]
            public int Id { get; set; }
           
            [Required]
            public int PatientId { get; set; }
            public int DoctorId { get; set; }
            public DateTime Date { get; set; }
            [Required]
            public string SlotTime { get; set; }
             public bool IsApproved { get; set; }
    }
}
