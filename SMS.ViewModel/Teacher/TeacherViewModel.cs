using SMS.Model.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModel.Teacher
{
    public class TeacherViewModel
    {
        /// <summary>
        /// get all teachers as list
        /// </summary>
        public IEnumerable<TeacherBO> TeachersList { get; set; }

        /// <summary>
        /// get one teacher's details
        /// </summary>
        public TeacherBO TeacherDetail { get; set; }

        public int? totalPages { get; set; }
    }
}
