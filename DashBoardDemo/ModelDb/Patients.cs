using System.ComponentModel.DataAnnotations;

namespace DashBoardDemo.ModelDb
{
    public class Patients
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Your Name")]
        [StringLength(3, ErrorMessage = "Name should be less than or equal to three characters.")]
        public string Name { get; set; }
        //public string DoctorName { get; set; }

        [Required(ErrorMessage = "Enter Your DOB.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        public int? Otp { get; set; }


    }
}
