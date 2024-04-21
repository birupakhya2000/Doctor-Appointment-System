using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DashBoardDemo.Models
{
    public class PatientLogin
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Please enter a username")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Please Enter Password")]
        public string passcode { get; set; }
        public string UserRole { get; set; }

    }
}
