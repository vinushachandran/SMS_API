using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
