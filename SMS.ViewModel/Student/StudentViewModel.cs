using SMS.Model.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModel.Student
{
    public class StudentViewModel
    {
        /// <summary>
		/// get all students as list
		/// </summary>
		public IEnumerable<StudentBO> AllStudentsList { get; set; }

        /// <summary>
        /// get one student details
        /// </summary>
        public StudentBO StudentDetail { get; set; }
    }
}
