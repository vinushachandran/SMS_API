using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SMS.Model.Allocation
{
    public class SubjectAllocationBO
    {
        /// <summary>
        /// ID of the subject allocation table
        /// </summary>
        [Key]
        public long? SubjectAllocationID { get; set; }

        /// <summary>
        /// Teacher id
        /// </summary>
        [Required(ErrorMessage = "Teacher is required")]
        [DisplayName("Teacher ID")]
        public long TeacherID { get; set; }

        /// <summary>
        /// Subject id
        /// </summary>
        [Required(ErrorMessage = "Subject is required")]
        [DisplayName("Registration Number")]
        public long SubjectID { get; set; }

        
    }
}
