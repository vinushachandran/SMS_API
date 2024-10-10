using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SMS.Model.Allocation
{
    public class StudentAllocationBO
    {
        /// <summary>
        /// Student allocation id
        /// </summary>
        [Key]
        public long? StudentAllocationID { get; set; }

        /// <summary>
        /// Student ID
        /// </summary>
        [Required(ErrorMessage = "Student is required")]
        [DisplayName("Student")]
        public long StudentID { get; set; }

        /// <summary>
        /// Subject allocation id
        /// </summary>
        [Required(ErrorMessage = "Subject is required")]
        [DisplayName("Subject")]
        public long SubjectAllocationID { get; set; }
    }
}
