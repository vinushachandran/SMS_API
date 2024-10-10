using System.ComponentModel;
/// <summary>
///
/// </summary>
/// <author>Vinusha</author>
///
using System.ComponentModel.DataAnnotations;

namespace SMS.Model.Student
{
    public class StudentBO
    {
        [Key]
        public long? StudentID { get; set; }
        [Required(ErrorMessage = "Student registration number is required")]
        [DisplayName("Registration No")]
        public string StudentRegNo { get; set; }
        [Required(ErrorMessage = "Student First Name is required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Student Last Name is required")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Student Display Name is required")]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        [DisplayName("Date Of Birth")]
        public DateTime DOB { get; set; }= DateTime.Today;
        [Required(ErrorMessage = "Address is required")]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Contact No is required")]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [DisplayName("Status")]
        public bool IsEnable { get; set; }
    }
}
