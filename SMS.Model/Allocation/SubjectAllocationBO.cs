using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Model.Subject;
using SMS.Model.Teacher;

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

        /// <summary>
        /// Navigation property for Teacher
        /// </summary>
        public TeacherBO Teacher { get; set; }

        /// <summary>
        /// Navigation property for Subject
        /// </summary>
        public SubjectBO Subject { get; set; }
    }
}
